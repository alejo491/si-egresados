using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AplicacionBase.Models
{
    public partial class Company
    {
        public Company()
        {
            this.Experiences = new List<Experience>();
            this.Vacancies = new List<Vacancy>();
        }

        public System.Guid Id { get; set; }


        [Required(ErrorMessage = "Por favor ingrese un nombre para la compañía")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un teléfono para la compañía")]
        [Display(Name = "Teléfono")]
        [RegularExpression(@"[0-9]{7,10}", ErrorMessage = " No tiene el formato de Telefono")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la dirección de la compañía")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un e-mail de la compañía")]
        [Display(Name = "E-Mail")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$", ErrorMessage = " No tiene el formato de un correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la ciudad donde se encuentra la compañía")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }


        [Required(ErrorMessage = "Por favor ingrese el sector empresarial donde se encuentra la compañía")]
        [Display(Name = "Sector")]
        public string Sector { get; set; }


        [Display(Name = "Experiencias")]
        public virtual ICollection<Experience> Experiences { get; set; }

        [Display(Name = "Vacantes")]
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
