using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using TH.RazorPages.Data;
using TH.RazorPages.Models;

namespace TH.RazorPages.Services
{
    public class ProductServices
    {
        private readonly DbContect _context;

        public ProductServices(DbContect context)
        {
            _context = context;
        }

        // Đăng ký người dùng mới
        public async Task<bool> RegisterAsync(User user)
        {
            var existingUser = await _context.Users
                .Where(u => u.Email == user.Email)
                .FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return false; // Email đã tồn tại
            }

            // Lưu người dùng vào cơ sở dữ liệu
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Đăng nhập người dùng
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _context.Users
                .Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}