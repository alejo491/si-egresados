using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Topic
    {
        public Topic()
        {
            this.Questions = new List<Question>();
            this.Surveys = new List<Survey>();
        }

        public System.Guid Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
