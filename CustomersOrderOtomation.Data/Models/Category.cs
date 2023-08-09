using CustomersOrderOtomation.Base.Model;

namespace CustomersOrderOtomation.Data.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public ICollection<ProductCategory>? Product_Categories { get; set; }

    }
}
