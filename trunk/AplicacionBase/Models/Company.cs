using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Company
    {
        public Company()
        {
            this.Experiences = new List<Experience>();
            this.Vacancies = new List<Vacancy>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Sector { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
