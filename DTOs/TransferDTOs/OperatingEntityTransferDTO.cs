using HaloBiz.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class OperatingEntityTransferDTO
    {
        public long Id { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        [Required, MinLength(3), MaxLength(255)]
        public string Description { get; set; }
        public long HeadId { get; set; }
        public virtual UserProfile Head { get; set; }
        [Required]
        public long DivisionId { get; set; }
        public virtual Division Division { get; set; }
        public IEnumerable<ServiceGroup> ServiceGroups { get; set; }
        public IEnumerable<StrategicBusinessUnit> StrategicBusinessUnits { get; set; }
    }
}
