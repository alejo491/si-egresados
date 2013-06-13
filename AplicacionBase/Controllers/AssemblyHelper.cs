using System;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    /// <summary>
    /// Helper con metodos para obtener atributos de clases y funciones de manera automatica mediante el uso de Ensambles
    /// </summary>
    public class AssemblyHelper
    {
        /// <summary>
        /// Atributo DbContext que realiza las consultas a la base de datos.
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();

        /// <summary>
        /// Metodo que devuelve una lista de tipos de datos que sean subclases del tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de dato</typeparam>
        /// <returns>Lista con todos los tipos de datos que son subclases del tipo T</returns>
        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(
                type => type.IsSubclassOf(typeof(T))).ToList();
        }

        
        /// <summary>
        /// Metodo que busca una clase con a traves de su nombre
        /// </summary>
        /// <param name="nombreClase">Nombre de la clase que se va a buscar</param>
        /// <returns>Lista de tipos de datos</returns>
        private static List<Type> GetClass(string nombreClase)
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(s=>s.Name == nombreClase).ToList();
        }


        /// <summary>
        /// Metodo que obtiene los atributos y su respectivo tipo de dato a partir de la clase "ConsultaGeneral" 
        /// </summary>
        /// <returns>Un diccionario con los atributos como llave y sus tipos de dato como valor</returns>
        public Dictionary<string, string> GetFieldsType()
        {
            var result = new Dictionary<string, string>();
            var clase = typeof(ConsultaGeneral);
            var att = clase.GetProperties();
            foreach (var fieldInfo in att)
            {
                result.Add(fieldInfo.Name, fieldInfo.PropertyType.ToString());
            }
            return result;
        }

        
        /// <summary>
        /// Metodo que guarda en la base de datos los nombres de los controladores existentes en el sistema, esto con para su uso en el modulo de seguridad
        /// </summary>
        /// <returns>Retorna verdadero si puede realizar el guardado, falso si ocurre un error</returns>
        public bool GetControllerNames()
        {
            try
            {
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

        /// <summary>
        /// Metodo que devuelve los roles que tienen acceso a determinado metodo de determinado controlador.
        /// </summary>
        /// <param name="controllerName">Nombre del controlador</param>
        /// <param name="methodName">Nombre del metodo del controlador</param>
        /// <returns>Un array de strings que contiene los roles que tienen acceso a ese metodo de ese controlador</returns>
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
