using System.Collections.Generic;
using System.Linq;
using AplicacionBase.Models;

namespace AplicacionBase.Models.ViewModels
{
    
    public class RoleMethodsViewModel
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        //The two properties
        public aspnet_Roles aspnet_Role { get; private set; }       
        public Dictionary<Method, bool> MethodChecklist  { get; private set; }

        //a constructor to create the above two properties upon instantiation
        public RoleMethodsViewModel(aspnet_Roles aspnet_Role, IEnumerable<Method> Method) 
        {
            this.aspnet_Role = aspnet_Role;
            MethodChecklist = new Dictionary<Method, bool>();
            var aspnetList = db.RoleMethods.Where(s => s.IdRole == aspnet_Role.RoleId);
            bool bandera = true;
            foreach (var asproles in Method)
            {

                foreach (var aspnetUsersInRolese in aspnetList)
                {
                    if (asproles.Id == aspnetUsersInRolese.IdRole)
                    {
                        MethodChecklist.Add(asproles, true);
                        bandera = false;
                        break;
                    }

                }
                
                if (bandera) 
                {
                    MethodChecklist.Add(asproles, false);
                }

                bandera = true;


                
            }
        }
    }
}