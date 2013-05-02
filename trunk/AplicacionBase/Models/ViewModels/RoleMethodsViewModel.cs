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
            foreach (var auxmethod in Method)
            {

                foreach (var methodslist in aspnetList)
                {
                    if (auxmethod.Id == methodslist.IdAction)
                    {
                        MethodChecklist.Add(auxmethod, true);
                        bandera = false;
                        break;
                    }

                }
                
                if (bandera) 
                {
                    MethodChecklist.Add(auxmethod, false);
                }

                bandera = true;


                
            }
        }
    }
}