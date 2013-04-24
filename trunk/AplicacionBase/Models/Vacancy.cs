using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Vacancy
    {
        public System.Guid Id { get; set; }
        public string Charge { get; set; }
        public string DayTrip { get; set; }
        public Nullable<decimal> HoursNumber { get; set; }
        public string ProfessionalProfile { get; set; }
        public System.DateTime PublicationDate { get; set; }
        public System.DateTime ClosingDate { get; set; }
        public string VacanciesNumber { get; set; }
        public decimal Salary { get; set; }
        public string Description { get; set; }
        public System.Guid IdUser { get; set; }
        public System.Guid IdCompanie { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
