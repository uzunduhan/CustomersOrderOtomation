using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CustomersOrderOtomation.Controllers
{
    public class ShopController : Controller
    {
        public ShopController()
        {
        }

        public IActionResult ShoppingCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<ProductForGetShopListDto>> GetShoppingCardProducts([FromServices] IProductService productService, [FromBody] List<ProductForAddShopListDto> prod)
        {
            return await productService.GetShoppingCardProducts(prod);
        }

        public async Task<IActionResult> AddShoppingCardProductsToShopList([FromServices] IShopListService shopListService, string cusName, string cusTableNo, [FromBody] List<ShopListAddShoppingCardProductsDto> dto)
        {
            var durum = await shopListService.AddShoppingCardProductsToShopList(cusName, cusTableNo, dto);
            return durum == true ? Json("basarili") : StatusCode(500, "Hata olustu");
        }
    }
}
