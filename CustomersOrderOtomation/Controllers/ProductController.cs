using AutoMapper;
using CustomersOrderOtomation.Data.DBOperations;
using CustomersOrderOtomation.Operations;
using CustomersOrderOtomation.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomersOrderOtomation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, DatabaseContext context, IMapper mapper)
        {
            this.productService = productService;
            this.context = context;
            this.mapper = mapper;
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
            var products = await context.ProductCategories.Where(x => x.CategoryId == categoryId).Select(x => x.Product).ToListAsync();

            clsHelper helper = new clsHelper();
            var vm = helper.ProductListToProductViewModel(products, mapper);

            return View("Shop", vm);
        }
    }
}
