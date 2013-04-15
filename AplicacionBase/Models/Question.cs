using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AplicacionBase.Models
{
    public partial class Question
    {
        public Question()
        {
            this.AnswerChoices = new List<AnswerChoice>();
            this.ExemplarsQuestions = new List<ExemplarsQuestion>();
        }

        [Display(Name = "Id Pregunta")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public System.Guid Id { get; set; }

        [Display(Name = "Id Tema")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public System.Guid IdTopic { get; set; }

        [Display(Name = "Tipo de Pregunta")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string Type { get; set; }

        [Display(Name = "Enunciado de la Pregunta")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [DataType(DataType.MultilineText)]
        public string Sentence { get; set; }

        [Display(Name = "Orden de Pregunta")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        //[Remote("ExisteNumero", "Questions", HttpMethod = "POST", ErrorMessage = "Ese numero ya esta asignado a otra respuesta, escoja otro")]
        public decimal QuestionNumber { get; set; }

        [Display(Name = "¿Es Obligatorio La Pregunta?")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public decimal IsObligatory { get; set; }

        public virtual ICollection<AnswerChoice> AnswerChoices { get; set; }
        public virtual ICollection<ExemplarsQuestion> ExemplarsQuestions { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
