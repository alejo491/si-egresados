using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class Elective
    {
        public Elective()
        {
            this.Studies = new List<Study>();
        }

        public System.Guid Id { get; set; }

        [Display(Name = "Electivas")]
        [RegularExpression(@"[A-Za-zñÑáéíóúÁÉÍÓÚ\s]*", ErrorMessage = "El formato es incorrecto")]
        public string Name { get; set; }
        public virtual ICollection<Study> Studies { get; set; }
    }
}
