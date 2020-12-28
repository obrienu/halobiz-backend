using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaloBiz.Model
{
    public class StrategicBusinessUnit
    {
        [Key]
        public long Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }
        [Required, MinLength(3)]
        public string Description { get; set; }
        [Required]
        public long OperatingEntityId { get; set; }
        public virtual OperatingEntity OperatingEntity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}