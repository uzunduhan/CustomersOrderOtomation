using CustomersOrderOtomation.Base.Dto;
using CustomersOrderOtomation.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CustomersOrderOtomation.Dto.Dtos
{
    public class ProductManagementDto : BaseDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public double? Price { get; set; }

        public ICollection<ProductCategory>? Product_Categories { get; set; }
    }
}
