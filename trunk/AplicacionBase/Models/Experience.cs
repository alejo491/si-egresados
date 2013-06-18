using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Por favor ingrese el cargo desempe�ado")]
        [Display(Name = "Cargo")]
        public string Charge { get; set; }

        [Required(ErrorMessage = "Por favor ingrese esta fecha")]
        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:D}")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Por favor ingrese esta fecha")]
        [Display(Name = "Fecha de Finalizaci�n")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:D}")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una descripci�n")]
        [Display(Name = "Descripci�n")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la compan�a donde labor�")]
        [Display(Name = "Compa��a")]
        public System.Guid IdCompanie { get; set; }



        public virtual Company Company { get; set; }
        public virtual ICollection<ExperiencesBoss> ExperiencesBosses { get; set; }
        public virtual User User { get; set; }
    }
}
