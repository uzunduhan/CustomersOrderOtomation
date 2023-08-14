using AutoMapper;
using CustomersOrderOtomation.Data.DBOperations;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Operations;
using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomersOrderOtomation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, IProductService productService, DatabaseContext context, IMapper mapper)
        {
            _logger = logger;
            this.categoryService = categoryService;
            this.productService = productService;
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShoppingCard()
        {
            return View();
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

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            return await categoryService.GetAllCategories();
        }

        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await context.ProductCategories.Where(x => x.CategoryId == categoryId).Select(x => x.Product).ToListAsync();

            clsHelper helper = new clsHelper();
            var vm = helper.ProductListToProductViewModel(products, mapper);

            return View("Shop", vm);
        }

        [HttpPost]
        public async Task<List<ProductForGetShopListDto>> GetShoppingCardProducts([FromBody] List<ProductForAddShopListDto> prod)
        {
            clsHelper helper = new clsHelper();

            var productDetailViewModels = await helper.GetShoppingCardProductList(prod, productService);


            return productDetailViewModels;
        }



    }
}