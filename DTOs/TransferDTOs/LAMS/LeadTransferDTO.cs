using System;
using System.Collections.Generic;
using HaloBiz.Model;
using HaloBiz.Model.LAMS;

namespace HaloBiz.DTOs.TransferDTOs.LAMS
{
    public class LeadTransferDTO
    {
        public long Id { get; set; }
        public string ReferenceNo { get; set; }
        public virtual LeadType LeadType { get; set; }
        public long LeadOriginId { get; set; }
        public virtual LeadOrigin LeadOrigin { get; set; }
        public string Industry { get; set; }
        public string RCNumber { get; set; }
        public string GroupName { get; set; }
        public GroupType GroupType { get; set; }
        public string LogoUrl { get; set; }
        public bool LeadCaptureStatus { get; set; } = false;
        public bool LeadQualificationStatus { get; set; } = false;
        public bool LeadOpportunityStatus { get; set; } = false;
        public bool LeadConversionStatus { get; set; } = false;
        public bool IsLeadDropped { get; set; }
        public string DropLearning { get; set; }
        public  DropReason DropReason { get; set; }
        public  LeadContactTransferDTO PrimaryContact { get; set; }
        public LeadContactTransferDTO SecondaryContact { get; set; }
        public IEnumerable<LeadKeyPersonTransferDTO> LeadKeyPersons { get; set; }
        public IEnumerable<LeadDivision> LeadDivisions { get; set; }
        public UserProfileTransferDTO CreatedBy { get; set; }
        
    }
}