using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Question
    {
        public Question()
        {
            this.AnswerChoices = new List<AnswerChoice>();
            this.ExemplarsQuestions = new List<ExemplarsQuestion>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdTopic { get; set; }
        public string Type { get; set; }
        public string Sentence { get; set; }
        public decimal QuestionNumber { get; set; }
        public decimal IsObligatory { get; set; }
        public virtual ICollection<AnswerChoice> AnswerChoices { get; set; }
        public virtual ICollection<ExemplarsQuestion> ExemplarsQuestions { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
