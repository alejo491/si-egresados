using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class UsersStep
    {
        public System.Guid UserId { get; set; }
        public System.Guid IdSteps { get; set; }
        public string Ok { get; set; }
        public virtual Step Step { get; set; }
        public virtual User User { get; set; }
    }
}
