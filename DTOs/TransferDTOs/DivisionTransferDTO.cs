using HaloBiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class DivisionTransferDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MissionStatement { get; set; }
        public long HeadId { get; set; }
        public virtual UserProfile Head { get; set; }
        public IEnumerable<OperatingEntity> OperatingEntities { get; set; }
    }
}
