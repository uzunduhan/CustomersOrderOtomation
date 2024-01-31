using AutoMapper;
using CustomersOrderOtomation.Data.Models;

namespace CustomersOrderOtomation.Service.Abstract
{
    public interface IHelper
    {
        Task<List<ViewModel.Product.ProductViewModel>> ProductListToProductViewModel(List<Product> products, IMapper mapper);
    }
}
