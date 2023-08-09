namespace CustomersOrderOtomation.ViewModel.Category
{
    public class CategoryDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductForCategoryDetailViewModel>? Products { get; set; }
    }
}
