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
    public class SecureControllersController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /SecureControllers/

        public ViewResult Index()
        {
            return View(db.SecureControllers.ToList());
        }

        //
        // GET: /SecureControllers/Details/5

        public ViewResult Details(Guid id)
        {
            SecureController securecontroller = db.SecureControllers.Find(id);
            return View(securecontroller);
        }

        //
        // GET: /SecureControllers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SecureControllers/Create

        [HttpPost]
        public ActionResult Create(SecureController securecontroller)
        {
            if (ModelState.IsValid)
            {
                securecontroller.Id = Guid.NewGuid();
                db.SecureControllers.Add(securecontroller);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(securecontroller);
        }
        
        //
        // GET: /SecureControllers/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            SecureController securecontroller = db.SecureControllers.Find(id);
            return View(securecontroller);
        }

        //
        // POST: /SecureControllers/Edit/5

        [HttpPost]
        public ActionResult Edit(SecureController securecontroller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(securecontroller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(securecontroller);
        }

        //
        // GET: /SecureControllers/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            SecureController securecontroller = db.SecureControllers.Find(id);
            return View(securecontroller);
        }

        //
        // POST: /SecureControllers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            SecureController securecontroller = db.SecureControllers.Find(id);
            db.SecureControllers.Remove(securecontroller);
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