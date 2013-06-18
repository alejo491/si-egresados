using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace AplicacionBase.Models
{
    public partial class ItemSurvey
    {
        public System.Guid Id { get; set; }
        public System.Guid IdReport { get; set; }

        /**************campos agregados para validacion**********************/
        [Display(Name = "Encuesta")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public System.Guid IdSurvey { get; set; }

        [Display(Name = "Tema")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public System.Guid IdTopic { get; set; }
        /**************************************/


       
        [Display(Name = "Pregunta")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string Question { get; set; }

        [Display(Name = "Tipo de Gràfico")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string GraphicType { get; set; }

        [Display(Name = "Numero de Item")]
        [Required(ErrorMessage = " ¡El campo es obligatorio y debe ser Numerico!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        [Range(1,double.MaxValue)]
       
        public decimal ItemNumber { get; set; }

        public string SQLQuey { get; set; }
        public virtual Report Report { get; set; }
    }
}
