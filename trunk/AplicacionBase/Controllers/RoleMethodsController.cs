using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AplicacionBase.Models;
using AplicacionBase.Models.ViewModels;


namespace AplicacionBase.Controllers
{
    public class RoleMethodsController : Controller
    {
        //
        // GET: /RoleMethods/
        private DbSIEPISContext db = new DbSIEPISContext();

       

        public ActionResult AssignRolesMethods(Guid id)
        {
            var aspnet_Role = db.aspnet_Roles.Find(id);
            if (aspnet_Role != null)
            {
                var Method = db.Methods;
                var model = new RoleMethodsViewModel(aspnet_Role, Method);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult AssignRolesMethods(Guid id, FormCollection postedForm)
        {
            List<Method> MethodToAdd = new List<Method>();
            aspnet_Roles aspnet_Role = db.aspnet_Roles.Find(id);
            foreach (var role in db.Methods)
            {
                if (postedForm[role.Name].ToString().Contains("true"))
                {
                    MethodToAdd.Add(role);
                }
            }
            ReassignMethod(aspnet_Role, MethodToAdd);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        
        public void ReassignMethod(aspnet_Roles aspnet_Role, List<Method> MethodToAdd)
        {           
            foreach (var cat in db.Methods)
            {
                RoleMethod v = db.RoleMethods.Find(aspnet_Role.RoleId, cat.Id);
                if (v != null)
                {
                    v.Method = null;
                    v.aspnet_Roles = null;
                    db.RoleMethods.Remove(v);                   
                }
                
            }
            db.SaveChanges();
            foreach (var category in MethodToAdd)
            {
                var auxUsersInrole = new RoleMethod(aspnet_Role.RoleId, category.Id);
                db.RoleMethods.Add(auxUsersInrole);
            }

        
            
        }
    }
}
