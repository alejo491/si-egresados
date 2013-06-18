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
    public class FileController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /File/

        public ViewResult Index()
        {
            return View(db.Files.ToList());
        }

        //
        // GET: /File/Details/5

        public ViewResult Details(Guid id)
        {
            File file = db.Files.Find(id);
            return View(file);
        }

        //
        // GET: /File/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /File/Create

        [HttpPost]
        public ActionResult Create(File file)
        {
            if (ModelState.IsValid)
            {
                file.Id = Guid.NewGuid();
                db.Files.Add(file);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(file);
        }
        
        //
        // GET: /File/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            File file = db.Files.Find(id);
            return View(file);
        }

        //
        // POST: /File/Edit/5

        [HttpPost]
        public ActionResult Edit(File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        //
        // GET: /File/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            File file = db.Files.Find(id);
            return View(file);
        }

        //
        // POST: /File/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            File file = db.Files.Find(id);
            db.Files.Remove(file);
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