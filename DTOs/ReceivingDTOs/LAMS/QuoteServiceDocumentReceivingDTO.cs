using System.ComponentModel.DataAnnotations;

namespace HaloBiz.DTOs.ReceivingDTOs
{
    public class QuoteServiceDocumentReceivingDTO : DocumentReceivingDTO
    {
        [Required]
        public long QuoteServiceId { get; set; }
    }
}