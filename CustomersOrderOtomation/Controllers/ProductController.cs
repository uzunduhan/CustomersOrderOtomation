using CustomersOrderOtomation.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrderOtomation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Shop()
        {
            var products = await productService.GetAllProducts();
            return View(products);
        }

        public async Task<IActionResult> ShopDetailed(int productId)
        {
            var produt = await productService.GetSingleProductByIdAsync(productId);
            return View(produt);
        }



        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await productService.GetProductsByCategory(categoryId);

            return View("Shop", products);
        }
    }
}
