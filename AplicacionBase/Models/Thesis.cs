using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class Thesis
    {
        public System.Guid IdStudies { get; set; }

        [Display(Name = "Título")]
        [RegularExpression(@"[A-Za-zñÑáéíóúÁÉÍÓÚ\s]*", ErrorMessage = "El formato es incorrecto")]
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Study Study { get; set; }
    }
}
