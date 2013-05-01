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

                var controllerNames = new List<string>();
                foreach (var controllerName in GetSubClasses<Controller>())
                {
                    var secureController = new SecureController { Id = Guid.NewGuid(), Name = controllerName.Name };
                    db.SecureControllers.Add(secureController);
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
                        if (!db.Methods.Any(s => s.FullName == method.FullName))
                        {
                            db.Methods.Add(method);
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
                        if (!db.Methods.Any(s => s.FullName == method.FullName))
                        {
                            db.Methods.Add(method);
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
    }
}
