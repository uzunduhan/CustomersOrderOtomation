using CustomersOrderOtomation.Base.Model;

namespace CustomersOrderOtomation.Data.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public double? Price { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ProductCategory>? Product_Categories { get; set; }
     

    }
}
