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
    public class SchoolController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /School/

        public ViewResult Index()
        {
            return View(db.Schools.ToList());
        }

        //
        // GET: /School/Details/5

        public ViewResult Details(Guid id)
        {
            School school = db.Schools.Find(id);
            return View(school);
        }

        //
        // GET: /School/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /School/Create

        [HttpPost]
        public ActionResult Create(School school)
        {
            if (ModelState.IsValid)
            {
                school.Id = Guid.NewGuid();
                db.Schools.Add(school);
                db.SaveChanges();
                return RedirectToAction("Create", "Study");  
            }

            return View(school);
        }
        
        //
        // GET: /School/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            School school = db.Schools.Find(id);
            return View(school);
        }

        //
        // POST: /School/Edit/5

        [HttpPost]
        public ActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(school);
        }
        //
        // GET: /School/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            School school = db.Schools.Find(id);
            return View(school);
        }

        //
        // POST: /School/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            School school = db.Schools.Find(id);
            db.Schools.Remove(school);
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