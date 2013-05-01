using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class SecureController
    {
        public SecureController()
        {
            this.Methods = new List<Method>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Method> Methods { get; set; }
    }
}
