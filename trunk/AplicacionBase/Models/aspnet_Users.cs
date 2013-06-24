using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa aspnet_Users
    /// </summary>
    public partial class aspnet_Users
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public aspnet_Users()
        {
            this.aspnet_PersonalizationPerUser = new List<aspnet_PersonalizationPerUser>();
            this.aspnet_UsersInRoles = new List<aspnet_UsersInRoles>();
        }
        /// <summary>
        /// Identificador de la aplicaci�n
        /// </summary>
        public System.Guid ApplicationId { get; set; }
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public System.Guid UserId { get; set; }
        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Apodo
        /// </summary>
        public string LoweredUserName { get; set; }
        /// <summary>
        /// alias
        /// </summary>
        public string MobileAlias { get; set; }
        /// <summary>
        /// M�todo para identificar si el usuario es an�nimo
        /// </summary>
        public bool IsAnonymous { get; set; }
        /// <summary>
        /// Fecha de la ultima actividad
        /// </summary>
        public System.DateTime LastActivityDate { get; set; }
        /// <summary>
        /// Colecci�n de Aplicaci�n
        /// </summary>
        public virtual aspnet_Applications aspnet_Applications { get; set; }
        /// <summary>
        /// Colecci�n de afiliaciones
        /// </summary>
        public virtual aspnet_Membership aspnet_Membership { get; set; }
        /// <summary>
        /// Colecci�n de personalizaciones de usuario
        /// </summary>
        public virtual ICollection<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        /// <summary>
        /// Colecci�n de perfil
        /// </summary>
        public virtual aspnet_Profile aspnet_Profile { get; set; }
        /// <summary>
        /// Colecci�n de roles de usuario
        /// </summary>
        public virtual ICollection<aspnet_UsersInRoles> aspnet_UsersInRoles { get; set; }
        /// <summary>
        /// Usuario
        /// </summary>
        public virtual User User { get; set; }
    }
}
