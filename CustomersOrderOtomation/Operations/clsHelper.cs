using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Service.Abstract;

namespace CustomersOrderOtomation.Operations
{
    public class clsHelper
    {

        public List<ViewModel.Product.ProductViewModel> ProductListToProductViewModel(List<Product> products, IMapper mapper)
        {
            List<ViewModel.Product.ProductViewModel> vm = mapper.Map<List<ViewModel.Product.ProductViewModel>>(products);
            return vm;
        }

        public async Task<List<ProductForGetShopListDto>> GetShoppingCardProductList(List<ProductForAddShopListDto> prod, IProductService productService)
        {
            List<ProductForGetShopListDto> productDetailViewModels = new List<ProductForGetShopListDto>();

            var prodList = prod
                .GroupBy(p => p.productId)
                 .Select(group => new
                  {
                      productId = group.Key,
                      piece = group.Sum(p => p.piece)
                  }).ToList();

            foreach (var product in prodList)
            {
                var getProduct = await productService.GetSingleProductByIdAsync(product.productId);

                ProductForGetShopListDto productForGetShopListDto = new ProductForGetShopListDto()
                {
                    Price = getProduct.Price,
                    Name = getProduct.Name,
                    Id = getProduct.Id,
                    Piece = product.piece

                };

                productDetailViewModels.Add(productForGetShopListDto);
            }

            return productDetailViewModels;
        }
    }
}
