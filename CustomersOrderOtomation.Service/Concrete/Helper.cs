using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Service.Abstract;

namespace CustomersOrderOtomation.Service.Concrete
{
    public class Helper : IHelper
    {

        public Helper()
        {
        }

        public async Task<List<ViewModel.Product.ProductViewModel>> ProductListToProductViewModel(List<Product> products, IMapper mapper)
        {
            List<ViewModel.Product.ProductViewModel> vm = mapper.Map<List<ViewModel.Product.ProductViewModel>>(products);
            return vm;
        }


    }
}
