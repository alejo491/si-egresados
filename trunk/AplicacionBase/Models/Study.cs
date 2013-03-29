using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Study
    {
        public Study()
        {
            this.Electives = new List<Elective>();
        }

        public System.Guid IdSchool { get; set; }
        public System.Guid IdUser { get; set; }
        public string Grade { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.Guid Id { get; set; }
        public virtual School School { get; set; }
        public virtual User User { get; set; }
        public virtual Thesis Thesis { get; set; }
        public virtual ICollection<Elective> Electives { get; set; }
    }
}
