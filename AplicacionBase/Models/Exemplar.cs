using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Exemplar
    {
        public Exemplar()
        {
            this.ExemplarsQuestions = new List<ExemplarsQuestion>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdSurveys { get; set; }
        public decimal ExemplarNumber { get; set; }
        public System.Guid IdSurveyed { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual Surveyed Surveyed { get; set; }
        public virtual ICollection<ExemplarsQuestion> ExemplarsQuestions { get; set; }
    }
}
