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
       
        [Display(Name = "Pregunta")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string Question { get; set; }
        [Display(Name = "Tipo de Gràfico")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string GraphicType { get; set; }
        [Display(Name = "Numero de Item")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public decimal ItemNumber { get; set; }
        public string SQLQuey { get; set; }
        public virtual Report Report { get; set; }
    }
}
