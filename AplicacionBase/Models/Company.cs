using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa una compa��a o empresa de la cual se requiere guardar alguna informaci�n
    /// </summary>
    public partial class Company
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Company()
        {
            this.Experiences = new List<Experience>();
            this.Vacancies = new List<Vacancy>();
        }

        /// <summary>
        /// Identificador �nico para cada compa��a registrada
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Nombre de la compa��a o empresa
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese un nombre para la compa��a")]

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        /// <summary>
        /// Tel�fono de la compa��a
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese un tel�fono para la compa��a")]
        [Display(Name = "Tel�fono")]
        [RegularExpression(@"[0-9]{7,10}", ErrorMessage = " No tiene el formato de Telefono")]
        public int Phone { get; set; }

        /// <summary>
        /// Direcci�n de alguna sede principal de la compa��a
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese la direcci�n de la compa��a")]
        [Display(Name = "Direcci�n")]
        public string Address { get; set; }

        /// <summary>
        /// Correo electr�nico para contactar con la compa��a
        /// </summary>        
        [Required(ErrorMessage = "Por favor ingrese un e-mail de la compa��a")]
        [Display(Name = "E-Mail")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$", ErrorMessage = " No tiene el formato de un correo electr�nico")]
        public string Email { get; set; }

        /// <summary>
        /// Ciudad donde se encuentra la compa��a, o alguna sede
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese la ciudad donde se encuentra la compa��a")]
        [Display(Name = "Ciudad")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string City { get; set; }

        /// <summary>
        /// Sector empresarial donde se ubica la compa��a
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese el sector empresarial donde se encuentra la compa��a")]
        [Display(Name = "Sector")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string Sector { get; set; }

        /// <summary>
        /// Colecci�n que hace referencia a personas que han trabajado en la compa��a
        /// </summary>
        [Display(Name = "Experiencias")]
        public virtual ICollection<Experience> Experiences { get; set; }

        /// <summary>
        /// Colecci�n que hace referencia a vacantes u ofertas de empleo que se ofrecen en la compa�ia
        /// </summary>
        [Display(Name = "Vacantes")]
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
