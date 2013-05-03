using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class aspnet_Roles
    {
        public aspnet_Roles()
        {
            this.aspnet_UsersInRoles = new List<aspnet_UsersInRoles>();
            this.Posts = new List<Post>();
            this.RoleMethods = new List<RoleMethod>();
        }

        public System.Guid ApplicationId { get; set; }
        public System.Guid RoleId { get; set; }
        [Display(Name = "Nombre del Rol")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string RoleName { get; set; }

        [Display(Name = "Nombre del Rol Abreviado")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string LoweredRoleName { get; set; }

        [Display(Name = "Descripciòn")]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string Description { get; set; }
        public virtual aspnet_Applications aspnet_Applications { get; set; }
        public virtual ICollection<aspnet_UsersInRoles> aspnet_UsersInRoles { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<RoleMethod> RoleMethods { get; set; }
    }
}
