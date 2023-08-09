using CustomersOrderOtomation.ViewModel.Product;

namespace CustomersOrderOtomation.ViewModel.Product
{
    public class ProductDetailViewModel
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public ICollection<CategoryForProductViewModel>? Categories { get; set; }
    }
}
