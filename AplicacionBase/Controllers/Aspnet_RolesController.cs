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
    public class Aspnet_RolesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Aspnet_Roles/

        public ViewResult Index()
        {
            var aspnet_roles = db.aspnet_Roles.Include(a => a.aspnet_Applications);
            return View(aspnet_roles.ToList());
        }

        //
        // GET: /Aspnet_Roles/Details/5

        public ViewResult Details(Guid id)
        {
            aspnet_Roles aspnet_roles = db.aspnet_Roles.Find(id);
            return View(aspnet_roles);
        }

        //
        // GET: /Aspnet_Roles/Create

        public ActionResult Create()
        {
            // ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName");
            return View();
        }

        //
        // POST: /Aspnet_Roles/Create

        [HttpPost]
        public ActionResult Create(aspnet_Roles aspnet_roles)
        {
            if (ModelState.IsValid)
            {
                aspnet_roles.ApplicationId = db.aspnet_Applications.First().ApplicationId;
                aspnet_roles.RoleId = Guid.NewGuid();
                db.aspnet_Roles.Add(aspnet_roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_roles.ApplicationId);
            return View(aspnet_roles);
        }

        //
        // GET: /Aspnet_Roles/Edit/5

        public ActionResult Edit(Guid id)
        {
            /* aspnet_Roles aspnet_roles = db.aspnet_Roles.Find(id);
             ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_roles.ApplicationId);
             return View(aspnet_roles);*/
            if (id != Guid.Empty && id != null)
            {
                aspnet_Roles answerChoice = db.aspnet_Roles.Find(id);
                var achoice = db.aspnet_Roles.Find(id);
                if (achoice != null)
                {
                    return View(answerChoice);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Aspnet_Roles/Edit/5

        [HttpPost]
        public ActionResult Edit(aspnet_Roles aspnet_roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnet_roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_roles.ApplicationId);
            return View(aspnet_roles);
        }

        //
        // GET: /Aspnet_Roles/Delete/5

        public ActionResult Delete(Guid id)
        {
            aspnet_Roles aspnet_roles = db.aspnet_Roles.Find(id);
            return View(aspnet_roles);
        }

        //
        // POST: /Aspnet_Roles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            aspnet_Roles aspnet_roles = db.aspnet_Roles.Find(id);
            db.aspnet_Roles.Remove(aspnet_roles);
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