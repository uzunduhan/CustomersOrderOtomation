using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomersOrderOtomation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShoppingCard()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult ShopDetailed(int productId)
        {
            return View();
        }

    }
}