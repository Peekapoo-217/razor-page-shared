using Microsoft.AspNetCore.Mvc;
using TH.RazorPages.Models;
using TH.RazorPages.Services;

namespace TH.RazorPages.Pages.Components.ProductBox
{
    public class ProductBox : ViewComponent
    {
        /*private readonly ProductServices _productService;
        public List<Product> products { get; set; }

        public ProductBox(ProductServices productService)
        {
            _productService = productService;

        }

        public async Task<IViewComponentResult> InvokeAsync(bool sapxeptang = true)
        {
            // Sử dụng GetProductsAsync nếu bạn muốn lấy sản phẩm một cách bất đồng bộ
            var products = await _productService.GetProductsAsync();  // Hoặc bạn có thể dùng GetProducts nếu là phương thức đồng bộ

            // Sắp xếp sản phẩm theo giá
            List<Product> _products = null;
            if (sapxeptang)
                _products = products.OrderBy(p => p.Price).ToList();
            else
                _products = products.OrderByDescending(p => p.Price).ToList();

            // Trả về view với danh sách sản phẩm
            return View<List<Product>>(_products);
        }*/
        List<Product> products = null;


        public IViewComponentResult Invoke()
        {
            return View<List<Product>>(products);
        }
    }
}
