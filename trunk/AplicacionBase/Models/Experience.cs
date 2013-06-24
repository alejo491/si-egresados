using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{   
    
    /// <summary>
    /// Clase que representa cada una de las experiencias laborales de un usuario
    /// </summary>
    public partial class Experience
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Experience()
        {
            this.ExperiencesBosses = new List<ExperiencesBoss>();
        }

        /// <summary>
        /// Identificador del usuario al que se asocia la experiencia laboral
        /// </summary>
        public System.Guid IdUser { get; set; }

        /// <summary>
        /// Identificador de la experiencia laboral
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Cargo que desempe�� la persona durante una experiencia laboral
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese el cargo desempe�ado")]
        [Display(Name = "Cargo")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string Charge { get; set; }

        /// <summary>
        /// Fecha de inicio en un cargo dentro de una compa��a
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese esta fecha")]
        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:d}")]
        public System.DateTime StartDate { get; set; }


        /// <summary>
        /// Fecha de finalizaci�n o retiro de un cargo dentro de una compa��a
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese esta fecha")]
        [Display(Name = "Fecha de Finalizaci�n")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:d}")]
        public System.DateTime EndDate { get; set; }


        /// <summary>
        /// Descripci�n de las labores desempe�adas durante el tiempo laborado en un cargo espec�fico
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese una descripci�n")]
        [Display(Name = "Descripci�n")]
        [RegularExpression(@"[(A-Za-z������������\s,;:.""''��0-9@�#$%/=�?!�~*|&)-_]*", ErrorMessage = "El formato es incorrecto")]
        public string Description { get; set; }


        /// <summary>
        /// Nombre de la compa��a contratante
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese la compan�a donde labor�")]
        [Display(Name = "Compa��a")]
        public System.Guid IdCompanie { get; set; }

        
        /// <summary>
        /// Objeto que referencia a la compa��a contratante
        /// </summary>
        public virtual Company Company { get; set; }


        /// <summary>
        /// Colecci�n de objetos que relacionan la experiencia laboral con el jefe que estuvo a cargo en la compan�a durante el per�odo laborado en la misma
        /// </summary>
        public virtual ICollection<ExperiencesBoss> ExperiencesBosses { get; set; }

        /// <summary>
        /// Objeto que referencia al usuario del que se gestionan sus experiencias laborales
        /// </summary>
        public virtual User User { get; set; }
    }
}
