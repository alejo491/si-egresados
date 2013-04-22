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
    public class ElectiveController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Elective/

        public ViewResult Index()
        {
            return View(db.Electives.ToList());
        }

        //
        // GET: /Elective/Details/5

        public ViewResult Details(Guid id)
        {
            Elective elective = db.Electives.Find(id);
            return View(elective);
        }

        //
        // GET: /Elective/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Elective/Create

        [HttpPost]
        public ActionResult Create(Elective elective)
        {
            if (ModelState.IsValid)
            {
                elective.Id = Guid.NewGuid();
                db.Electives.Add(elective);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(elective);
        }
        
        //
        // GET: /Elective/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Elective elective = db.Electives.Find(id);
            return View(elective);
        }

        //
        // POST: /Elective/Edit/5

        [HttpPost]
        public ActionResult Edit(Elective elective)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elective).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(elective);
        }

        //
        // GET: /Elective/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Elective elective = db.Electives.Find(id);
            return View(elective);
        }

        //
        // POST: /Elective/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Elective elective = db.Electives.Find(id);
            db.Electives.Remove(elective);
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