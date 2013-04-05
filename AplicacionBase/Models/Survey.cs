using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace AplicacionBase.Models
{
    public partial class Survey
    {
        public Survey()
        {
            this.Exemplars = new List<Exemplar>();
            this.SurveysTopics = new List<SurveysTopic>();
        }

        public System.Guid Id { get; set; }

        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [MaxLength(50, ErrorMessage = "No pueder tener mas de 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [MaxLength(50, ErrorMessage = "No pueder tener mas de 50 caracteres")]
        public string Aim { get; set; }
        public virtual ICollection<Exemplar> Exemplars { get; set; }
        
        public virtual ICollection<SurveysTopic> SurveysTopics { get; set; }
        
        
    }
}

