using System.Collections.Generic;
using System.Linq;

namespace AplicacionBase.Models.ViewModels
{
    
    public class UsersInRolesViewModel
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        //The two properties
        public aspnet_Users aspnet_User { get; private set; }       
        public Dictionary<aspnet_Roles, bool> aspnet_RolesChecklist  { get; private set; }

        //a constructor to create the above two properties upon instantiation
        public UsersInRolesViewModel(aspnet_Users aspnet_User, IEnumerable<aspnet_Roles> aspnet_Roles) 
        {
            this.aspnet_User = aspnet_User;
            aspnet_RolesChecklist = new Dictionary<aspnet_Roles, bool>();

            foreach (var asproles in aspnet_Roles)
            {
                var UserRole = db.aspnet_UsersInRoles.Find(aspnet_User.UserId, asproles.RoleId);
                if (UserRole != null) //.Any(producttypecategory => asproles.RoleId == producttypecategory.UserId))
                {
                    aspnet_RolesChecklist.Add(asproles, true);
                }
                else
                {
                     aspnet_RolesChecklist.Add(asproles, false);
                }

               /* if(!found)
                    aspnet_RolesChecklist.Add(asproles, false);*/
            }
        }
    }
}