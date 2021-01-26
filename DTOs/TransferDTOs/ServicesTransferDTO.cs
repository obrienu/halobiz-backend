using System.Collections.Generic;
using HaloBiz.Model.ManyToManyRelationship;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class ServicesTransferDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ServiceCode { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; } = 1;
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } = 0;
        public double VAT { get; set; }
        public double BillableAmount { get; set; }
        public long ServiceCategoryId { get; set; }
        public long ServiceGroupId { get; set; }
        public long OperatingEntityId { get; set; }
        public long DivisionId { get; set; }
        public TargetTransferDTO Target { get; set; }
        public ServiceTypeTransferDTO ServiceType { get; set; }
        public IList<RequiredServiceDocumentTransferDTO> RequiredServiceDocument { get; set; }
        public IList<RequredServiceQualificationElementTransferDTO> RequiredServiceFields { get; set; }
       
    }
}