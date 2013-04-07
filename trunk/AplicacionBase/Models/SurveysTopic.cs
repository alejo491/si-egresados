using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AplicacionBase.Models
{
    public partial class SurveysTopic
    {

        public System.Guid IdSurveys { get; set; }

        [Display(Name = "Tema")]
        public System.Guid IdTopic { get; set; }

        [Display(Name = "Orden")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public decimal TopicNumber { get; set; }        
        public virtual Survey Survey { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
