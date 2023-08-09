using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.ViewModel.Product;

namespace CustomersOrderOtomation.Service.Abstract
{
    public interface IProductService
    {
        Task<ProductDetailViewModel> GetSingleProductByIdAsync(int id);
        Task<List<ProductViewModel>> GetAllProducts();
        Task UpdateProductAsync(int id, ProductDto updateResource);
        Task DeleteProductAsync(int id);
        Task AddProductAsync(ProductDto updateResource);

    }
}
