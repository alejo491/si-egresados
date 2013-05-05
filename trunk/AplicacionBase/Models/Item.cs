using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AplicacionBase.Models
{
    public partial class Item
    {
        public System.Guid Id { get; set; }
        public System.Guid IdReport { get; set; }

        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string TableName { get; set; }

        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string FieldName { get; set; }

        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public decimal PageNumber { get; set; }

        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public virtual Report Report { get; set; }
    }
}
