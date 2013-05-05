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
    public class ReportsController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Reports/

        public ViewResult Index()
        {
            var te = db.aspnet_Users.First(u => u.UserName == HttpContext.User.Identity.Name).UserId;
            var reports = db.Reports.Where(r => r.IdUser == te);
            return View(reports.ToList());
        }

        //
        // GET: /Reports/Details/5

        public ViewResult Details(Guid id)
        {
            Report report = db.Reports.Find(id);
            var user = db.Users.Find(report.IdUser);

           // ViewBag.Re = id;

            ViewBag.nombre = user.FirstNames + " " + user.LastNames;

            return View(report);
        }

        //
        // GET: /Reports/Create

        public ActionResult Create()
        {
            //ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber");
            return View();
        }

        //
        // POST: /Reports/Create

        [HttpPost]
        public ActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                report.IdUser = db.aspnet_Users.First(u => u.UserName == HttpContext.User.Identity.Name).UserId;
                report.ReportDate = DateTime.Now;
                report.Id = Guid.NewGuid();
                db.Reports.Add(report);
                db.SaveChanges();
                TempData["Create"] = "Se registró correctamente el reporte !";
                return RedirectToAction("Index");
            }

            return View(report);
        }

        //
        // GET: /Reports/Edit/5

        public ActionResult Edit(Guid id)
        {
            Report report = db.Reports.Find(id);

            return View(report);
        }

        //
        // POST: /Reports/Edit/5

        [HttpPost]
        public ActionResult Edit(Report report)
        {
            if (ModelState.IsValid)
            {
                report.IdUser = db.aspnet_Users.First(u => u.UserName == HttpContext.User.Identity.Name).UserId;
                report.ReportDate = DateTime.Now;
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Update"] = "Se actualizó correctamente el reporte !";
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", report.IdUser);
            return View(report);
        }

        //
        // GET: /Reports/Delete/5

        public ActionResult Delete(Guid id)
        {
            Report report = db.Reports.Find(id);
            return View(report);
        }

        //
        // POST: /Reports/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            db.SaveChanges();
            TempData["Delete"] = "Se eliminó correctamente el reporte !";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}