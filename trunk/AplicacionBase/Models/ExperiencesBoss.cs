using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class ExperiencesBoss
    {
        public System.Guid Id { get; set; }
        public System.Guid IdBoss { get; set; }
        public System.Guid IdExperiences { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la fecha de inicio")]
        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:D}")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la fecha de finalización")]
        [Display(Name = "Fecha de Finalización")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:D}")]
        public System.DateTime EndDate { get; set; }

        [Display(Name = "Jefe")]
        [Required(ErrorMessage = "Por favor seleccione un jefe")]
        public virtual Boss Boss { get; set; }


        public virtual Experience Experience { get; set; }
    }
}
