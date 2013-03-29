using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Experience
    {
        public Experience()
        {
            this.ExperiencesBosses = new List<ExperiencesBoss>();
        }

        public System.Guid IdUser { get; set; }
        public System.Guid Id { get; set; }
        public string Charge { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Description { get; set; }
        public System.Guid IdCompanie { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<ExperiencesBoss> ExperiencesBosses { get; set; }
        public virtual User User { get; set; }
    }
}
