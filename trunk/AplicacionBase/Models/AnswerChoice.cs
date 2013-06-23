using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class AnswerChoice
    {
        public AnswerChoice()
        {
            this.Answers = new List<Answer>();
        }

        [Display(Name = "Id Opción")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public System.Guid Id { get; set; }

        [Display(Name = "Id Pregunta")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public System.Guid IdQuestion { get; set; }

        [Display(Name = "Enunciado")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [DataType(DataType.MultilineText)]
        public string Sentence { get; set; }

        [Display(Name = "Valor Cuantitativo")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [Range(1, double.MaxValue)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public decimal NumericValue { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string Type { get; set; }

        [Display(Name = "Número de Orden")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        [Range(0, double.MaxValue, ErrorMessage = "Tiene que ser un numero positivo")]
        public decimal AnswerNumber { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
}
