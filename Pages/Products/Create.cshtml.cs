using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TH.RazorPages.Data;
using TH.RazorPages.Models;

namespace TH.RazorPages.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly DbContect _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateModel(DbContect context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet()
        {
            var categories = _context.Categorys.ToList();
            if (categories == null || !categories.Any())
            {
                ModelState.AddModelError(string.Empty, "Không có danh mục nào khả dụng. Vui lòng thêm danh mục trước.");
                return Page();
            }

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return Page();
        }





        [BindProperty]
        public Product Product { get; set; }

        // Thêm hình ảnh vào sản phẩm
        public async Task<IActionResult> OnPostAsync(List<IFormFile> productImages)
        {


            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"Key: {state.Key}, Value: {state.Value.AttemptedValue}, Errors: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
                }

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name", Product.CategoryId);
                return Page();
            }




            // Thêm sản phẩm vào cơ sở dữ liệu
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            // Kiểm tra và lưu hình ảnhs
            if (productImages != null && productImages.Count > 0)
            {
                foreach (var file in productImages)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);


                        // Lưu file vào thư mục wwwroot/images
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Thêm đường dẫn ảnh vào cơ sở dữ liệu
                        var imgProduct = new ImgProduct
                        {
                            Path = "/images/" + fileName,
                            ProductId = Product.Id
                        };

                        _context.ImgProducts.Add(imgProduct);
                    }
                }

                // Lưu các ảnh vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }

            // Redirect về trang danh sách sau khi thêm thành công
            return RedirectToPage("./Index");
        }
    }
}