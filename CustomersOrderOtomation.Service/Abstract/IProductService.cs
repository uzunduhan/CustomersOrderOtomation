using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.ViewModel.Product;
using Microsoft.AspNetCore.Http;

namespace CustomersOrderOtomation.Service.Abstract
{
    public interface IProductService
    {
        Task<ProductDetailViewModel> GetSingleProductByIdAsync(int id);
        Task<List<ProductViewModel>> GetAllProducts();
        Task UpdateProductAsync(int id, ProductDto updateResource);
        Task DeleteProductAsync(int id);
        Task AddProductAsync(ProductDto updateResource);
        Task<List<ProductViewModel>> GetProductsByCategory(int categoryId);
        Task<List<ProductForGetShopListDto>> GetShoppingCardProducts(List<ProductForAddShopListDto> prod);
        Task<List<ProductViewModelManagement>> GetAllProductsForManagement();
        Task<bool> CreateOrUpdateProductAsync(IFormCollection parameters);

    }
}
