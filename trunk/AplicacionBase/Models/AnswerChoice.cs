using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class AnswerChoice
    {
        public AnswerChoice()
        {
            this.Answers = new List<Answer>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdQuestion { get; set; }
        public string Sentence { get; set; }
        public decimal NumericValue { get; set; }
        public string Type { get; set; }
        public decimal AnswerNumber { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
