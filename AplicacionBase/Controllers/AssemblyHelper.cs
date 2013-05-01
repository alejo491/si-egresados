using System;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    public class AssemblyHelper
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }

        public bool GetControllerNames()
        {
            try
            {
                /*
                foreach (var roleMethod in db.RoleMethods)
                {
                    db.RoleMethods.Remove(roleMethod);
                    //db.SaveChanges();
                }

                foreach (var method in db.Methods)
                {
                    db.Methods.Remove(method);
                   // db.SaveChanges();
                }

                foreach (var secureController in db.SecureControllers)
                {
                    db.SecureControllers.Remove(secureController);
                    //db.SaveChanges();
                }
                */
                var controllerNames = new List<string>();
                foreach (var controllerName in GetSubClasses<Controller>())
                {
                    Dictionary<string, Method> metodos = new Dictionary<string, Method>();
                    var secureController = new SecureController { Id = Guid.NewGuid(), Name = controllerName.Name };
                    if (db.SecureControllers.Count(s => s.Name == secureController.Name) == 0)
                    {
                        db.SecureControllers.Add(secureController);
                    }
                    else
                    {
                        secureController = db.SecureControllers.First(s => s.Name == secureController.Name);                        
                    }

                    foreach (var methodInfo in controllerName.GetMethods().Where(s=>s.ReturnType == typeof(ActionResult)))
                    {
                        var nombreMetodo = methodInfo.Name;
                        var method = new Method
                        {
                            Id = Guid.NewGuid(),
                            IdController = secureController.Id,
                            Name = nombreMetodo,
                            FullName = nombreMetodo + "-" + secureController.Name
                        };
                        if (!metodos.ContainsKey(method.FullName))
                        {
                            metodos.Add(method.FullName, method);                           
                        }
                        
                    }

                    foreach (var methodInfo in controllerName.GetMethods().Where(s => s.ReturnType == typeof(ViewResult)))
                    {
                        var nombreMetodo = methodInfo.Name;
                        var method = new Method
                        {
                            Id = Guid.NewGuid(),
                            IdController = secureController.Id,
                            Name = nombreMetodo,
                            FullName = nombreMetodo + "-" + secureController.Name
                        };
                        if (!metodos.ContainsKey(method.FullName))
                        {
                            metodos.Add(method.FullName, method);
                        }
                    }
                    foreach (var method in metodos)
                    {
                        if (db.Methods.Where(s=>s.FullName == method.Key).Count() == 0)
                        {
                            db.Methods.Add(metodos[method.Key]);
                        }
                        
                    }
                    
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public string[] GetRolesMethods(string controllerName, string methodName)
        {
            List<String> roles = new List<string>();
            var secureController = db.SecureControllers.First(s => s.Name == controllerName);
            if (secureController != null)
            {
                var method = db.Methods.First(s => s.IdController == secureController.Id && s.Name == methodName);
                if (method != null)
                {
                    var roleMethods = db.RoleMethods.Where(r => r.IdAction == method.Id);
                    if (roleMethods.Any())
                    {
                        foreach (var roleMethod in roleMethods)
                        {
                            var name = db.aspnet_Roles.First(s => s.RoleId == roleMethod.IdRole).RoleName;
                            roles.Add(name);
                        }
                    }
                    
                }
                
            }
            return roles.ToArray();
        }
    }
}
