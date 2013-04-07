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
            var aspnetList = db.aspnet_UsersInRoles.Where(s => s.UserId == aspnet_User.UserId);
            bool bandera = true;
            foreach (var asproles in aspnet_Roles)
            {

                foreach (var aspnetUsersInRolese in aspnetList)
                {
                    if (asproles.RoleId == aspnetUsersInRolese.RoleId)
                    {
                        aspnet_RolesChecklist.Add(asproles, true);
                        bandera = false;
                        break;
                    }

                }
                
                if (bandera) 
                {
                    aspnet_RolesChecklist.Add(asproles, false);
                }

                bandera = true;


                
            }
        }
    }
}