using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TH.RazorPages.Models;

namespace TH.RazorPages.Pages
{
    public class ContactPageModel : PageModel
    {
        [BindProperty]
        public Contact contact { get; set; }
        public ContactPageModel() 
        {
            contact = new Contact();
        }
        public string thongbao {  get; set; }
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                thongbao = "Dữ liệu gửi không hợp lệ";
            }
            else
            {
                thongbao = "Dữ liệu không hợp lệ";
            }
        }
        public void OnGet() { }
    }
}
