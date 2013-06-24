using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa las electivas
    /// </summary>
    public partial class Elective
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Elective()
        {
            this.Studies = new List<Study>();
        }

        /// <summary>
        /// C�digo de la electiva
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Nombre de la electiva
        /// </summary>
        [Display(Name = "Electivas")]
        //[RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
         public string Name { get; set; }

        /// <summary>
        /// Colecci�n de estudios de la electiva.
        /// </summary>
        public virtual ICollection<Study> Studies { get; set; }
    }
}
