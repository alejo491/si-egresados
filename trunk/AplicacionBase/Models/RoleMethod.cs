using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class RoleMethod
    {
        public System.Guid IdRole { get; set; }
        public System.Guid IdAction { get; set; }
        public virtual aspnet_Roles aspnet_Roles { get; set; }
        public virtual Method Method { get; set; }


        public RoleMethod( Guid RoleId, Guid ActionId)
        {
            this.IdAction = ActionId;
            this.IdRole = RoleId;
            
        }

        public RoleMethod()
        {
            aspnet_Roles = new aspnet_Roles();
            Method = new Method();
        }
    }
}
