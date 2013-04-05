using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Surveyed
    {
        public Surveyed()
        {
            this.Exemplars = new List<Exemplar>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Exemplar> Exemplars { get; set; }
    }
}
