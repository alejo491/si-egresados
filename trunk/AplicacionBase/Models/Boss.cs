using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Boss
    {
        public Boss()
        {
            this.ExperiencesBosses = new List<ExperiencesBoss>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<ExperiencesBoss> ExperiencesBosses { get; set; }
    }
}
