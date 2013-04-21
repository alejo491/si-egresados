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
    public class StudyController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Study/

        public ViewResult Index()
        {
            var studies = db.Studies.Include(s => s.School).Include(s => s.User).Include(s => s.Thesis);
            return View(studies.ToList());
        }

        //
        // GET: /Study/Details/5

        public ViewResult Details(Guid id)
        {
            Study study = db.Studies.Find(id);
            return View(study);
        }

        //
        // GET: /Study/Create

        public ActionResult Create()
        {
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name");
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber");
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title");
            return View();
        } 

        //
        // POST: /Study/Create

        [HttpPost]
        public ActionResult Create(Study study)
        {
            if (ModelState.IsValid)
            {
                study.Id = Guid.NewGuid();
                db.Studies.Add(study);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name", study.IdSchool);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", study.IdUser);
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title", study.Id);
            return View(study);
        }
        
        //
        // GET: /Study/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Study study = db.Studies.Find(id);
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name", study.IdSchool);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", study.IdUser);
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title", study.Id);
            return View(study);
        }

        //
        // POST: /Study/Edit/5

        [HttpPost]
        public ActionResult Edit(Study study)
        {
            if (ModelState.IsValid)
            {
                db.Entry(study).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name", study.IdSchool);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", study.IdUser);
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title", study.Id);
            return View(study);
        }

        //
        // GET: /Study/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Study study = db.Studies.Find(id);
            return View(study);
        }

        //
        // POST: /Study/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Study study = db.Studies.Find(id);
            db.Studies.Remove(study);
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