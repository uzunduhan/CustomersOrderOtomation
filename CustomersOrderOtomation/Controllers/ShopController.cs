using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Operations;
using CustomersOrderOtomation.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CustomersOrderOtomation.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService productService;
        private readonly IShopListService shopListService;

        public ShopController(IProductService productService, IShopListService shopListService)
        {
            this.productService = productService;
            this.shopListService = shopListService;
        }

        public IActionResult ShoppingCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<ProductForGetShopListDto>> GetShoppingCardProducts([FromBody] List<ProductForAddShopListDto> prod)
        {
            clsHelper helper = new clsHelper();

            var productDetailViewModels = await helper.GetShoppingCardProductList(prod, productService);


            return productDetailViewModels;
        }

        public async Task<IActionResult> AddShoppingCardProductsToShopList(string cusName, string cusTableNo, [FromBody] List<ShopListAddShoppingCardProductsDto> dto)
        {

            ShopListDto shopListDto = new ShopListDto()
            {
                OrderCustomerName = cusName,
                OrderTableNumber = cusTableNo,
                ShopListProductsData = JsonSerializer.Serialize(dto)
            };

            try
            {
                await shopListService.AddShopListAsync(shopListDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hata olustu");
            }


            return Json("basarili");
        }
    }
}
