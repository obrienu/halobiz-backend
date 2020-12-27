using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaloBiz.Model_
{
    public class UserProfile
    {
        [Key]
        public long Id { get; set; }
        [Required, MaxLength(20), MinLength(3), RegularExpression("\\w{3,20}")]
        public string FirstName { get; set; }
        [RegularExpression("\\w{3,20}")]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("\\d{10,15}")]
        public string MobileNumber { get; set; }
        [Required, MaxLength(255)]
        public string ImageUrl { get; set; }
        [EmailAddress]
        public string AltEmail { get; set; }
        [RegularExpression("\\d{10,15}")]
        public string AltMobileNumber { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(255)]
        public string LinkedInHandle { get; set; }
        [MaxLength(255)]
        public string FacebookHandle  { get; set; }
        [MaxLength(255)]
        public string TwitterHandle { get; set; }
        [MaxLength(255)]
        public string InstagramHandle { get; set; }
        public long StaffId { get; set; }
        public bool ProfileStatus { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}