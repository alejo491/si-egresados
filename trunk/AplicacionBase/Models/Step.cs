using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Step
    {
        public Step()
        {
            this.UsersSteps = new List<UsersStep>();
        }

        public System.Guid Id { get; set; }
        public decimal SOrder { get; set; }
        public string SPath { get; set; }
        public virtual ICollection<UsersStep> UsersSteps { get; set; }
    }
}
