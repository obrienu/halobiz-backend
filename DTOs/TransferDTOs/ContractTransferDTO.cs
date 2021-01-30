using System.Collections.Generic;
using HaloBiz.Model;
using HaloBiz.Model.LAMS;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class ContractTransferDTO
    {
        public long Id { get; set; }
        public string ReferenceNo { get; set; }
        public long CustomerDivisionId { get; set; }
        public CustomerDivision CustomerDivisions { get; set; }
        public long QuoteId { get; set; }
        public virtual Quote Quote { get; set; }
        public IEnumerable<ContractServiceTransferDTO> ContractServices { get; set; }
    }
}