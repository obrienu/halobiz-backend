using System.ComponentModel.DataAnnotations;

namespace HaloBiz.DTOs.ReceivingDTO
{
    public class UserProfileReceivingDTO
    {
    
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
    }
}