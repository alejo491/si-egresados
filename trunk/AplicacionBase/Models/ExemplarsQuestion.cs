using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class ExemplarsQuestion
    {
        public ExemplarsQuestion()
        {
            this.Answers = new List<Answer>();
        }

        public System.Guid IdQuestion { get; set; }
        public System.Guid IdExemplar { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Exemplar Exemplar { get; set; }
        public virtual Question Question { get; set; }
    }
}
