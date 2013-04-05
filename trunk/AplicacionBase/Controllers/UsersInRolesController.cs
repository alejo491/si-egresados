using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{ 
    public class UsersInRolesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /aspnet_UsersInRoles/

        public ViewResult Index()
        {
            var aspnet_usersinroles = db.aspnet_UsersInRoles;//.Include(a => a.aspnet_Roles).Include(a => a.aspnet_Users);
            
            foreach (var aspnetUsersInRolese in aspnet_usersinroles)
            {
                Guid d = aspnetUsersInRolese.UserId;
                Console.WriteLine(d);
            }
            return View(aspnet_usersinroles.ToList());
        }

        //
        // GET: /aspnet_UsersInRoles/Details/5

        public ViewResult Details(Guid id)
        {
            aspnet_UsersInRoles aspnet_usersinroles = db.aspnet_UsersInRoles.Find(id);
            return View(aspnet_usersinroles);
        }

        //
        // GET: /aspnet_UsersInRoles/Create

        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.aspnet_Roles, "RoleId", "RoleName");
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName");
            return View();
        } 

        //
        // POST: /aspnet_UsersInRoles/Create

        [HttpPost]
        public ActionResult Create(aspnet_UsersInRoles aspnet_usersinroles)
        {
            if (ModelState.IsValid)
            {
                aspnet_usersinroles.UserId = Guid.NewGuid();
                db.aspnet_UsersInRoles.Add(aspnet_usersinroles);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.RoleId = new SelectList(db.aspnet_Roles, "RoleId", "RoleName", aspnet_usersinroles.RoleId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_usersinroles.UserId);
            return View(aspnet_usersinroles);
        }
        
        //
        // GET: /aspnet_UsersInRoles/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            aspnet_UsersInRoles aspnet_usersinroles = db.aspnet_UsersInRoles.Find(id);
            ViewBag.RoleId = new SelectList(db.aspnet_Roles, "RoleId", "RoleName", aspnet_usersinroles.RoleId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_usersinroles.UserId);
            return View(aspnet_usersinroles);
        }

        //
        // POST: /aspnet_UsersInRoles/Edit/5

        [HttpPost]
        public ActionResult Edit(aspnet_UsersInRoles aspnet_usersinroles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnet_usersinroles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.aspnet_Roles, "RoleId", "RoleName", aspnet_usersinroles.RoleId);
            ViewBag.UserId = new SelectList(db.aspnet_Users, "UserId", "UserName", aspnet_usersinroles.UserId);
            return View(aspnet_usersinroles);
        }

        //
        // GET: /aspnet_UsersInRoles/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            aspnet_UsersInRoles aspnet_usersinroles = db.aspnet_UsersInRoles.Find(id);
            return View(aspnet_usersinroles);
        }

        //
        // POST: /aspnet_UsersInRoles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            aspnet_UsersInRoles aspnet_usersinroles = db.aspnet_UsersInRoles.Find(id);
            db.aspnet_UsersInRoles.Remove(aspnet_usersinroles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}