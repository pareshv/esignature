using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLogic.Models
{
    public class DocumentHistory
    {
        public DateTime? EdittedOn { get; set; }

        public DateTime? ReceivedOn { get; set; }

        public int RecipientCount { get; set; }

        [Display(Name = "DocumentInformation")]
        public virtual int DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual DocumentInformation DocumentInformations { get; set; }
    }
}
