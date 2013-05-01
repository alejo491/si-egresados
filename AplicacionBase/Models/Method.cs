using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Method
    {
        public Method()
        {
            this.RoleMethods = new List<RoleMethod>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public System.Guid IdController { get; set; }
        public virtual ICollection<RoleMethod> RoleMethods { get; set; }
        public virtual SecureController SecureController { get; set; }
    }
}
