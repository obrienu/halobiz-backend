using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HaloBiz.Helpers;

namespace HaloBiz.Model.LAMS
{
    public class ContractService
    {
        [Key]
        public long Id { get; set; }
        public DateTime? ActivationDate { get; set; } = null;
        public GuardType? GuardType { get; set; } = null;
        [StringLength(500)]
        public string Location { get; set; }
        public DateTime? EntryDateTime { get; set; } = null;
        [StringLength(500)]
        public string PickUp { get; set; }
        [StringLength(500)]
        public string DropOff { get; set; }
        public DateTime? ServiceDateTime { get; set; } = null;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long Quantity { get; set; } = 1;
        [StringLength(1000)]
        public string ProblemStatement { get; set; }
        public bool IsGuardTouringRequired { get; set; } = false;
        public double Discount { get; set; } = 0.0;
        public double Price { get; set; }
        public double VAT { get; set; } = 0.0;
        public double BillableAmount  { get; set; } = 0.0;
        public double Budget { get; set; } = 0.0;
        public long? PaymentCycle { get; set; }
        public int? InvoiceSendInterval { get; set; }
        public DateTime? InvoiceSendDate { get; set; }
        public bool IsConvertedToContractService { get; set; } =false;
        public long ServiceId { get; set; }
        public Services Service { get; set; }
        public long QuoteId { get; set; }
        public virtual Quote Quote { get; set; }
        public long OfficeId { get; set; }
        public virtual Office Office { get; set; }
        public long BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public long QuoteServiceId { get; set; }
        public virtual QuoteService QuoteService { get; set; }
        public IEnumerable<ClosureDocument> ClosureDocuments { get; set; }
        public IEnumerable<SBUToContractServiceProportion> SBUToContractServiceProportions { get; set; }
        public VersionType Version { get; set; } = VersionType.Latest;
        public long CreatedById { get; set; }
        public virtual UserProfile CreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}