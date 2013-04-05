using System.Collections.Generic;
using System.Linq;

namespace AplicacionBase.Models.ViewModels
{
    public class UsersInRolesViewModel
    {
        //The two properties
        public aspnet_Users aspnet_User { get; private set; }       
        public Dictionary<aspnet_Roles, bool> aspnet_RolesChecklist  { get; private set; }

        //a constructor to create the above two properties upon instantiation
        public UsersInRolesViewModel(aspnet_Users aspnet_User, IEnumerable<aspnet_Roles> aspnet_Roles) 
        {
            bool found;
            this.aspnet_User = aspnet_User;
            aspnet_RolesChecklist = new Dictionary<aspnet_Roles, bool>();

            foreach (var category in aspnet_Roles)
            {
                found = false;
                if (aspnet_User.aspnet_UsersInRoles.Any(producttypecategory => category.RoleId == producttypecategory.UserId))
                {
                    aspnet_RolesChecklist.Add(category, true);
                    found = true;
                }
                if(!found)
                    aspnet_RolesChecklist.Add(category, false);
            }
        }
    }
}