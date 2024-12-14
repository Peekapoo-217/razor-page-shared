using System.ComponentModel.DataAnnotations;

namespace TH.RazorPages.Models
{
    public class ImgProduct
    {
        [Key]
        public int Id { get; set; }

        // Đường dẫn ảnh
        public string Path { get; set; }

        // Mối quan hệ với Product (một ảnh thuộc về một sản phẩm)
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
