﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class RequredServiceQualificationElementTransferDTO : BaseSetupTransferDTO
    {
        public ServiceCategoryTransferDTO ServiceCategory { get; set; }
    }
}