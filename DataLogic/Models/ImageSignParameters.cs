using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLogic.Models
{
    public class ImageSignParameters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }
        public int XIndent { get; set; }
        public int YIndent { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public double Opacity { get; set; }
        public string AttachmentPath { get; set; }
        public byte[] ImageStream { get; set; }
        public int PageNumber { get; set; }
    }
}
