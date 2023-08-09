using System.ComponentModel.DataAnnotations.Schema;

namespace CustomersOrderOtomation.Base.Model
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
