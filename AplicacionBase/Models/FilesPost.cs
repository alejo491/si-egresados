using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class FilesPost
    {
        public System.Guid IdPost { get; set; }
        public System.Guid IdFile { get; set; }
        public int Main { get; set; }
        public string Type { get; set; }
        public virtual File File { get; set; }
        public virtual Post Post { get; set; }
    }
}
