using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class School
    {
        public School()
        {
            this.Studies = new List<Study>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Study> Studies { get; set; }
    }
}
