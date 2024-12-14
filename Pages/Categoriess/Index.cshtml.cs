using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TH.RazorPages.Models;

namespace TH.RazorPages.Pages.Categoriess
{
    public class IndexModel : PageModel
    {
        private readonly TH.RazorPages.Data.DbContect _context;

        public IndexModel(TH.RazorPages.Data.DbContect context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categorys.ToListAsync();
        }
    }
}