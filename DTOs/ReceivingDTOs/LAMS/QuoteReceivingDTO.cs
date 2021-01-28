using HaloBiz.Helpers;
using HaloBiz.Model;
using HaloBiz.Model.LAMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaloBiz.DTOs.ReceivingDTOs
{
    public class QuoteReceivingDTO
    {
        [StringLength(50)]
        public string ReferenceNo { get; set; }
        public long LeadDivisionId { get; set; }
        public bool IsConvertedToContract { get; set; } = true;
        public VersionType Version { get; set; } = VersionType.Latest;
    }
}