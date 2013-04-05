using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class aspnet_UsersInRoles
    {
        public System.Guid UserId { get; set; }
        public System.Guid RoleId { get; set; }
       
        public virtual aspnet_Roles aspnet_Roles { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }

        public aspnet_UsersInRoles(Guid UserId, Guid RoleId)
        {
            this.UserId = UserId;
            this.RoleId = RoleId;
            
        }

        public aspnet_UsersInRoles()
        {
            aspnet_Roles = new aspnet_Roles();
            aspnet_Users = new aspnet_Users();
        }
    }
}
