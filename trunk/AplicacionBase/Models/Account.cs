using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Account
    {
        public System.Guid Id { get; set; }
        public System.Guid IdUser { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        public virtual User User { get; set; }
    }
}
