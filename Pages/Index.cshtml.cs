using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TH.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        // Thêm phương thức xử lý Logout
        public IActionResult OnPostLogout()
        {
            // Xóa session
            HttpContext.Session.Clear();

            // Chuyển hướng đến trang đăng nhập
            return RedirectToPage("/Users/Login");
        }
    }
}