using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa una persona que ha ejercido como jefe de un usuario, en un cargo desempe�ado durante un per�odo de tiempo
    /// </summary>
    public partial class Boss
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Boss()
        {
            this.ExperiencesBosses = new List<ExperiencesBoss>();
        }

        /// <summary>
        /// Identificador �nico para cada jefe registrado
        /// </summary>

        public System.Guid Id { get; set; }

        /// <summary>
        /// Nombre completo del jefe
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese el nombre de este jefe")]
        [Display(Name = "Nombre")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string Name { get; set; }

        /// <summary>
        /// Correo electr�nico de contacto del jefe
        /// </summary>      
        [Required(ErrorMessage = "Por favor el correo electr�nico de contacto")]
        [Display(Name = "E-Mail")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$", ErrorMessage = " No tiene el formato de un correo electr�nico")]
        public string Email { get; set; }

        /// <summary>
        /// N�mero telef�nico de contacto del jefe
        /// </summary>  
        [Display(Name = "Tel�fono")]
        [RegularExpression(@"[0-9]{7,10}", ErrorMessage = " No tiene el formato de Telefono")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Colecci�n que indica las relaciones jefe-empleado que ha tenido un jefe
        /// </summary> 
        public virtual ICollection<ExperiencesBoss> ExperiencesBosses { get; set; }
    }
}
