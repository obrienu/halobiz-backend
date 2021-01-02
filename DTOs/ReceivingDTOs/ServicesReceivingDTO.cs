using System.ComponentModel.DataAnnotations;

namespace HaloBiz.DTOs.ReceivingDTOs
{
    public class ServicesReceivingDTO
    {
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        [Required, MinLength(3), MaxLength(255)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public int Quantity { get; set; } = 1;
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } = 0;
        public double VAT { get; set; }
        public double BillableAmount { get; set; }
        [Required]
        public long ServiceCategoryId { get; set; }
        [Required]
        public long ServiceGroupId { get; set; }
        [Required]
        public long OperatingEntityId { get; set; }
        [Required]
        public long DivisionId { get; set; }
    }
}