using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class ExperiencesBoss
    {
        public System.Guid Id { get; set; }
        public System.Guid IdBoss { get; set; }
        public System.Guid IdExperiences { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public virtual Boss Boss { get; set; }
        public virtual Experience Experience { get; set; }
    }
}
