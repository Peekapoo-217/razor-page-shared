using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TH.RazorPages.Data;
using TH.RazorPages.Models;

namespace TH.RazorPages.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly DbContect _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EditModel(DbContect context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                            .Include(p => p.ImgProducts)
                            .FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name");
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name");
                return Page();
            }



            //var productEdit = _context.Products.FirstOrDefault(P => P.Id == id);
            var productEdit = await _context.Products.FindAsync(id);
            if (productEdit == null)
            {
                return NotFound();
            }
            // Update product details
            productEdit.Name = product.Name;
            productEdit.Description = product.Description;
            productEdit.Price = product.Price;
            // Save changes to ensure Product.Id is valid
            await _context.SaveChangesAsync();

            // Xử lý xóa ảnh nếu có
            var deleteImageIds = Request.Form["DeleteImageIds"].ToList();
            if (deleteImageIds.Any())
            {
                var imagesToDelete = _context.ImgProducts
                    .Where(img => deleteImageIds.Contains(img.Id.ToString())).ToList();

                // Xóa ảnh trong thư mục
                foreach (var img in imagesToDelete)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", img.Path.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.ImgProducts.RemoveRange(imagesToDelete);
            }

            // Kiểm tra nếu có ảnh mới được tải lên
            var newImages = Request.Form.Files.Where(file => file.Length > 0).ToList();
            if (newImages.Any())
            {
                // Kiểm tra thư mục lưu trữ ảnh
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                foreach (var newImage in newImages)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                    var filePath = Path.Combine(uploadDirectory, fileName);

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await newImage.CopyToAsync(stream);
                        }

                        var imgProduct = new ImgProduct
                        {
                            ProductId = productEdit.Id,
                            Path = "/images/" + fileName // Đảm bảo đường dẫn đúng
                        };

                        _context.ImgProducts.Add(imgProduct);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, $"Error uploading image '{newImage.FileName}': {ex.Message}");
                        return Page();
                    }
                }
            }
            else
            {
                // Nếu không có ảnh mới, bạn không cần thông báo lỗi
                Console.WriteLine("No new images uploaded.");
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Cập nhật thông tin sản phẩm
            //_context.Attach(Product).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }




        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
