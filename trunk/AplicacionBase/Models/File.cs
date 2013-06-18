using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class File
    {
        public File()
        {
            this.FilesPosts = new List<FilesPost>();
        }

        public System.Guid Id { get; set; }
        public string Patch { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public virtual ICollection<FilesPost> FilesPosts { get; set; }
    }
}
