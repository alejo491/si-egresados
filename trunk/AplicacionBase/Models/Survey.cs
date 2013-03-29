using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Survey
    {
        public Survey()
        {
            this.Exemplars = new List<Exemplar>();
            this.Topics = new List<Topic>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Aim { get; set; }
        public virtual ICollection<Exemplar> Exemplars { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
