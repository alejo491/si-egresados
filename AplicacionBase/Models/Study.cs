using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class Study
    {
        public Study()
        {
            this.Electives = new List<Elective>();
        }

        [Required]
        [DisplayName("Instituci�n")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public System.Guid IdSchool { get; set; }

        [DisplayName("Programa")]
        public System.String Programs { get; set; }

        public System.Guid IdUser { get; set; }

        [Required]
        [DisplayName("T�tulo")]
        public string Grade { get; set; }

        [Required]
        [DisplayName("Fecha de Inicio (dd/mm/yyyy)")]
        public System.DateTime StartDate { get; set; }

        [Required]
        [DisplayName("Fecha de Finalizaci�n (dd/mm/yyyy)")]
        public System.DateTime EndDate { get; set; }
        public System.Guid Id { get; set; }

        public virtual School School { get; set; }
        public virtual User User { get; set; }
        public virtual Thesis Thesis { get; set; }
        public virtual ICollection<Elective> Electives { get; set; }
    }
}
