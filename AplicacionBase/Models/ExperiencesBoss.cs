using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que guarda informaci�n sobre los jefes que estuvieron a cargo en una determinada experiencia laboral
    /// </summary>
    public partial class ExperiencesBoss
    {
        /// <summary>
        /// Identificador de la clase
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Identificador del jefe
        /// </summary>
        public System.Guid IdBoss { get; set; }

        /// <summary>
        /// Identificador de la experiencia laboral
        /// </summary>
        public System.Guid IdExperiences { get; set; }

        /// <summary>
        /// Fecha de inicio del mandato del jefe sobre el empleado
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese la fecha de inicio")]
        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:D}")]
        public System.DateTime StartDate { get; set; }

        /// <summary>
        /// Fecha de finalizaci�n del mandato del jefe sobre el empleado
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese la fecha de finalizaci�n")]
        [Display(Name = "Fecha de Finalizaci�n")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:D}")]
        public System.DateTime EndDate { get; set; }

        /// <summary>
        /// Objeto que referencia a un jefe
        /// </summary>
        public virtual Boss Boss { get; set; }

        /// <summary>
        /// Objeto que referencia a una experiencia laboral
        /// </summary>
        public virtual Experience Experience { get; set; }
    }
}
