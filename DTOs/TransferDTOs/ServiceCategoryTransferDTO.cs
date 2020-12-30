using System.Collections.Generic;
using HaloBiz.Model;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class ServiceCategoryTransferDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ServicesTransferDTO> Services { get; set; }
    }
}