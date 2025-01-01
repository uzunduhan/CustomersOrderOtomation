using CustomersOrderOtomation.Base.Dto;
using System.ComponentModel.DataAnnotations;

namespace CustomersOrderOtomation.Dto.Dtos
{
    public class ProductDto : BaseDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required]
        public string? ImageUrl { get; set; }
    }
}
