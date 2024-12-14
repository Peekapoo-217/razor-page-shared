using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TH.RazorPages.Models;
using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
    [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự.")]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Giá là bắt buộc.")]
    [Range(0.0, 10000.00, ErrorMessage = "Giá sản phẩm phải nằm trong khoảng từ 0.01 đến 10000.00.")]
    public decimal Price { get; set; } = 0;

    [Required(ErrorMessage = "Danh mục là bắt buộc.")]
    public int CategoryId { get; set; }
    [ValidateNever]
    public Category Category { get; set; }

    // Không yêu cầu hình ảnh khi tạo sản phẩm
    public List<ImgProduct> ImgProducts { get; set; } = new List<ImgProduct>();
}