using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLogic.Models
{
    public class DocumentStatus
    {
        public bool Approval { get; set; }

        public DateTime? ApprovalDate { get; set; }


        [Display(Name = "DocumentInformation")]
        public virtual int DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual DocumentInformation DocumentInformations { get; set; }
    }
}
