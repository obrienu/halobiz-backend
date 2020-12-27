using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaloBiz.Model_
{
    public class Branch
    {
        [Key]
        public long Id { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        [Required, MinLength(3), MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public int HeadId { get; set; }
        public UserProfile Head { get; set; }
        public IEnumerable<Office> Offices { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}