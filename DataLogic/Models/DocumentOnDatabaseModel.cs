using System;
using System.Collections.Generic;
using System.Text;

namespace DataLogic.Models
{
    public class DocumentOnDatabaseModel:DocumentInformation
    {
        public byte[] Content { get; set; }
    }
}
