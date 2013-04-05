using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Answer
    {
        public System.Guid Id { get; set; }
        public System.Guid IdAnswer { get; set; }
        public System.Guid IdQuestion { get; set; }
        public System.Guid IdExemplar { get; set; }
        public string TextValue { get; set; }
        public virtual AnswerChoice AnswerChoice { get; set; }
        public virtual ExemplarsQuestion ExemplarsQuestion { get; set; }
    }
}
