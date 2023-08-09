using CustomersOrderOtomation.Base.Model;

namespace CustomersOrderOtomation.Data.Models
{
    public class ShopList : BaseModel
    {
        public string OrderCustomerName { get; set; }
        public string OrderTableNumber { get; set; }
        public string ShopListProductsData { get; set; }
        public bool? IsComplete { get; set; } = false;
    }
}
