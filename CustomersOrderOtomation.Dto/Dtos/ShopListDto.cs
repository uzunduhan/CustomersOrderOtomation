namespace CustomersOrderOtomation.Dto.Dtos
{
    public class ShopListDto
    {
        public string? OrderCustomerName { get; set; }
        public string? OrderTableNumber { get; set; }
        public string? ShopListProductsData { get; set; }
        public bool? IsComplete { get; set; } = false;
    }
}
