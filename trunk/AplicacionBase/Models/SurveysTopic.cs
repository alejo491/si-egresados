using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AplicacionBase.Models
{
    public partial class SurveysTopic
    {
        public System.Guid IdSurveys { get; set; }
        public System.Guid IdTopic { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        [Remote("ExisteNumero", "SurveysTopics", HttpMethod = "POST", ErrorMessage = "Ese numero ya esta asignado a otro tema, escoja otro")]
        public decimal TopicNumber { get; set; }        
        public virtual Survey Survey { get; set; }        
        public virtual Topic Topic { get; set; }
    }
}
