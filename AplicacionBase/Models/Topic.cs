using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class Topic
    {
        public Topic()
        {
            this.Questions = new List<Question>();
            this.SurveysTopics = new List<SurveysTopic>();
        }

        public System.Guid Id { get; set; }
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        [Display(Name="Descripción")]
        [MaxLength(50, ErrorMessage = "No puede tener más de 50 caracteres")]
        public string Description { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<SurveysTopic> SurveysTopics { get; set; }
    }
}
