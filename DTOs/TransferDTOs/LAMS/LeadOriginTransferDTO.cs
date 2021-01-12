using System.ComponentModel.DataAnnotations;

namespace HaloBiz.DTOs.TransferDTOs.LAMS
{
    public class LeadOriginTransferDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Caption { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public long LeadTypeId { get; set; }
    }
}