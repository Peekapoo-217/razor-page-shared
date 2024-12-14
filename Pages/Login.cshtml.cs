using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TH.RazorPages.Data;
using TH.RazorPages.Models;

namespace TH.RazorPages.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly DbContect _context;

        [BindProperty]
        public User Input { get; set; }

        public CreateUserModel(DbContect context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(Input);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
