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
    public class ThesisController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Thesis/

        public ViewResult Index()
        {
            var theses = db.Theses.Include(t => t.Study);
            return View(theses.ToList());
        }

        //
        // GET: /Thesis/Details/5

        public ViewResult Details(Guid id)
        {
            Thesis thesis = db.Theses.Find(id);
            return View(thesis);
        }

        //
        // GET: /Thesis/Create

        public ActionResult Create()
        {
            ViewBag.IdStudies = new SelectList(db.Studies, "Id", "Grade");
            return View();
        } 

        //
        // POST: /Thesis/Create

        [HttpPost]
        public ActionResult Create(Thesis thesis)
        {
            if (ModelState.IsValid)
            {
                thesis.IdStudies = Guid.NewGuid();
                db.Theses.Add(thesis);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdStudies = new SelectList(db.Studies, "Id", "Grade", thesis.IdStudies);
            return View(thesis);
        }
        
        //
        // GET: /Thesis/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Thesis thesis = db.Theses.Find(id);
            ViewBag.IdStudies = new SelectList(db.Studies, "Id", "Grade", thesis.IdStudies);
            return View(thesis);
        }

        //
        // POST: /Thesis/Edit/5

        [HttpPost]
        public ActionResult Edit(Thesis thesis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thesis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdStudies = new SelectList(db.Studies, "Id", "Grade", thesis.IdStudies);
            return View(thesis);
        }

        //
        // GET: /Thesis/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Thesis thesis = db.Theses.Find(id);
            return View(thesis);
        }

        //
        // POST: /Thesis/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Thesis thesis = db.Theses.Find(id);
            db.Theses.Remove(thesis);
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