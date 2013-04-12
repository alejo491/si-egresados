using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionBase.Models.ViewModels
{
    public class AnswerViewModel
    {
        
        //public aspnet_Users User { get; private set; }
        //public Dictionary<Question, String> Questions { get; set;}

        //public AnswerViewModel(aspnet_Users user, IEnumerable<Question> questions)
        //{
        //    this.User = user;
        //    foreach (var topic in questions)
        //    {
        //        this.Questions.Add(topic, "");
        //    }
        //}

        public Dictionary<Question, String> Questions { get; set; }

        public AnswerViewModel(IEnumerable<Question> questions)
        {    
            this.Questions = new Dictionary<Question, string>();
            foreach (var topic in questions)
            {
                this.Questions.Add(topic, "");
            }
        }
    }
}