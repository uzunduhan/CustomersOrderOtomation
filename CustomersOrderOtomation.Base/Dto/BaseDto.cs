using System.ComponentModel.DataAnnotations;

namespace CustomersOrderOtomation.Base.Dto
{
    public abstract class BaseDto
    {
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }


        [MaxLength(500)]
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        public bool? IsActive { get; set; } = true;
    }
}
