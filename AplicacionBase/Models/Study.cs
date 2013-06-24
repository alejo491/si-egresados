using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa los estudios
    /// </summary>
    public partial class Study
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Study()
        {
            this.Electives = new List<Elective>();
        }

        /// <summary>
        /// Identificador de la instituci�n
        /// </summary>
        [DisplayName("Instituci�n")]
        public System.Guid IdSchool { get; set; }

        /// <summary>
        /// NombrePrograma
        /// </summary>
        [DisplayName("Programa")]
        public System.String Programs { get; set; }

        /// <summary>
        /// Identificador del Usuario
        /// </summary>
        public System.Guid IdUser { get; set; }

        /// <summary>
        /// T�tulo
        /// </summary>
        [DisplayName("T�tulo")]
        public string Grade { get; set; }

        /// <summary>
        /// Fecha de Inicio
        /// </summary>
        [DisplayName("Fecha de Inicio")]
        public System.DateTime StartDate { get; set; }

        /// <summary>
        /// Fecha de Finalizaci�n
        /// </summary>
        [DisplayName("Fecha de Finalizaci�n")]
        public System.DateTime EndDate { get; set; }

        /// <summary>
        /// Identificador del estudio
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Instituci�n 
        /// </summary>
        public virtual School School { get; set; }

        /// <summary>
        /// usuario
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Tesis
        /// </summary>
        public virtual Thesis Thesis { get; set; }

        /// <summary>
        /// Colecci�n de electivas del estudio
        /// </summary>
        public virtual ICollection<Elective> Electives { get; set; }
    }
}
