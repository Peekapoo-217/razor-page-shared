using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TH.RazorPages.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly TH.RazorPages.Data.DbContect _context;

        public DetailsModel(TH.RazorPages.Data.DbContect context)
        {
            _context = context;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Include hình ảnh liên quan (ImgProducts)
            Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ImgProducts) // Bao gồm hình ảnh của sản phẩm
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}