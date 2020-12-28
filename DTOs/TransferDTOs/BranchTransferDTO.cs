﻿using HaloBiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class BranchTransferDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HeadId { get; set; }
        public UserProfile Head { get; set; }
        public IEnumerable<Office> Offices { get; set; }
    }
}