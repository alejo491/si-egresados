using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa las instituciones
    /// </summary>
    public partial class School
    {
        /// <summary>
        /// Constructor de la Clase
        /// </summary>
        public School()
        {
            this.Studies = new List<Study>();
        }

        /// <summary>
        /// Identificador de la Instituci�n
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Nombre de la Instituci�n
        /// </summary>
        [DisplayName("Nombre de la Instituci�n")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string Name { get; set; }

        /// <summary>
        /// Colecci�n de estudios de la instituci�n
        /// </summary>
        public virtual ICollection<Study> Studies { get; set; }
    }
}
