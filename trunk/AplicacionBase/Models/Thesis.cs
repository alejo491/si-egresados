using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Thesis
    {
        public System.Guid IdStudies { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Study Study { get; set; }
    }
}
