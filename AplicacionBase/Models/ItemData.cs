using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class ItemData
    {
        public ItemData()
        {
            this.Fields = new List<Field>();
            this.Filters = new List<Filter>();
            this.GroupOptions = new List<GroupOption>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdReport { get; set; }
        [Display(Name = "Enunciado")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [MinLength(1, ErrorMessage = "No pueder tener mas de 300 caracteres")]
        public string Sentence { get; set; }

        public string SQLQuey { get; set; }

        [Display(Name = "Tipo de Gráfico")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string GraphicType { get; set; }

        [Display(Name = "Numero de Item")]
        [Required(ErrorMessage = " ¡El campo es obligatorio y debe ser Numerico!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        [Range(1, double.MaxValue, ErrorMessage = "Tiene que ser un numero positivo")]
        public decimal ItemNumber { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }
        public virtual ICollection<GroupOption> GroupOptions { get; set; }
        public virtual Report Report { get; set; }
    }
}
