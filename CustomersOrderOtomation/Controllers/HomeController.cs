using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrderOtomation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, IProductService productService)
        {
            _logger = logger;
            this.categoryService = categoryService;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShoppingCard()
        {
            return View();
        }

        public async Task <IActionResult> Shop()
        {
            var products = await productService.GetAllProducts();
            return View(products);
        }

        public IActionResult ShopDetailed(int productId)
        {
            return View();
        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            return await categoryService.GetAllCategories();
        }

    }
}