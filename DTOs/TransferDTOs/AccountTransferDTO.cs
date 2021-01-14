using HaloBiz.Model;
using HaloBiz.Model.AccountsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.DTOs.TransferDTOs
{
    public class AccountTransferDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public long Alias { get; set; }
        public IEnumerable<AccountMaster> AccountMasters { get; set; }
        [Required]
        public bool IsDebitBalance { get; set; }
        [Required]
        public long AccountClassId { get; set; }
        public virtual AccountClass AccountClass { get; set; }
        public long CreatedById { get; set; }
        public virtual UserProfile CreatedBy { get; set; }
    }
}
