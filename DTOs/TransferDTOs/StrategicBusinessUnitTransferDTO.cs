using HaloBiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class StrategicBusinessUnitTransferDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long OperatingEntityId { get; set; }
        public virtual OperatingEntity OperatingEntity { get; set; }
    }
}
