using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa la Tesis
    /// </summary>
    public partial class Thesis
    {
        /// <summary>
        /// Identificador del Estudio
        /// </summary>
        public System.Guid IdStudies { get; set; }

        /// <summary>
        /// T�tulo de la tesis
        /// </summary>
        [Display(Name = "T�tulo")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string Title { get; set; }

        /// <summary>
        /// Descrioci�n de la tesis
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Estudio asignado a la tesis
        /// </summary>
        public virtual Study Study { get; set; }
    }
}
