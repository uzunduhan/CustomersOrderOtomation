using CustomersOrderOtomation.Base.Dto;
using System.ComponentModel.DataAnnotations;

namespace CustomersOrderOtomation.Dto.Dtos
{
    public class CategoryDto : BaseDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
