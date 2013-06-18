using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class Boss
    {
        public Boss()
        {
            this.ExperiencesBosses = new List<ExperiencesBoss>();
        }

        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el nombre de este jefe")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor el correo electrónico de contaco")]
        [Display(Name = "E-Mail")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$", ErrorMessage = " No tiene el formato de un correo electrónico")]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [RegularExpression(@"[0-9]{7,10}", ErrorMessage = " No tiene el formato de Telefono")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<ExperiencesBoss> ExperiencesBosses { get; set; }
    }
}
