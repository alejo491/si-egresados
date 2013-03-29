using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Elective
    {
        public Elective()
        {
            this.Studies = new List<Study>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Study> Studies { get; set; }
    }
}
