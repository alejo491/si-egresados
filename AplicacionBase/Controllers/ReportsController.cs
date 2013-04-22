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
            var reports = db.Reports.Include(r => r.User);
            return View(reports.ToList());
        }

        //
        // GET: /Reports/Details/5

        public ViewResult Details(Guid id)
        {
            Report report = db.Reports.Find(id);
            return View(report);
        }

        //
        // GET: /Reports/Create

        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber");
            return View();
        } 

        //
        // POST: /Reports/Create

        [HttpPost]
        public ActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                report.Id = Guid.NewGuid();
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", report.IdUser);
            return View(report);
        }
        
        //
        // GET: /Reports/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Report report = db.Reports.Find(id);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", report.IdUser);
            return View(report);
        }

        //
        // POST: /Reports/Edit/5

        [HttpPost]
        public ActionResult Edit(Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
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
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}