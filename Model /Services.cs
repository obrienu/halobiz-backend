using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaloBiz.Model_
{
    public class Services
    {
        [Key]
        public long Id { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        [Required, MinLength(3), MaxLength(255)]
        public string Description { get; set; }
        public int Quantity { get; set; } = 1;
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } = 0;
        public double VAT { get; set; }
        public double BillableAmount { get; set; }
        [Required]
        public long ServiceCategoryId { get; set; }
        public virtual ServiceCategory ServiceCategory { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}