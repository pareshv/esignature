using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLogic.Models
{
    public class DocumentInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentId { get; set; }

        public string Name { get; set; }

        public string FileType { get; set; }

        public DateTime? SubmissionDate { get; set; }
    }
}
