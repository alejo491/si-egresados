using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AplicacionBase.Models;
using AplicacionBase.Models.ViewModels;

namespace AplicacionBase.Controllers
{
    public class UsersRolesController : Controller
    {
        //
        // GET: /UsersRoles/
        private DbSIEPISContext db = new DbSIEPISContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AssignUserRoles(Guid id)
        {
            var aspnet_User = db.aspnet_Users.Find(id);
            if (aspnet_User != null)
            {
                var aspnet_Roles = db.aspnet_Roles;
                var model = new UsersInRolesViewModel(aspnet_User, aspnet_Roles);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult AssignUserRoles(Guid id, FormCollection postedForm)
        {
            List<aspnet_Roles> aspnet_RolesToAdd = new List<aspnet_Roles>();
            aspnet_Users aspnet_User = db.aspnet_Users.Find(id);
            foreach (var role in db.aspnet_Roles)
            {
                if (postedForm[role.RoleName].ToString().Contains("true"))
                {
                    aspnet_RolesToAdd.Add(role);
                }
            }
            Reassignaspnet_Roles(aspnet_User, aspnet_RolesToAdd);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        
        public void Reassignaspnet_Roles(aspnet_Users aspnet_User, List<aspnet_Roles> aspnet_RolesToAdd)
        {           
            foreach (var cat in db.aspnet_Roles)
            {
                aspnet_UsersInRoles v = db.aspnet_UsersInRoles.Find(aspnet_User.UserId, cat.RoleId);
                if (v != null)
                {
                    v.aspnet_Roles = null;
                    v.aspnet_Users = null;
                    db.aspnet_UsersInRoles.Remove(v);                   
                }
                
            }
            db.SaveChanges();
            foreach (var category in aspnet_RolesToAdd)
            {
                var auxUsersInrole = new aspnet_UsersInRoles(aspnet_User.UserId, category.RoleId);
                db.aspnet_UsersInRoles.Add(auxUsersInrole);
            }

        
            
        }
    }
}
