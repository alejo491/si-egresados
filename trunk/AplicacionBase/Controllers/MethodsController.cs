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
    public class MethodsController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Methods/

        public ViewResult Index()
        {
            var methods = db.Methods.Include(m => m.SecureController);
            return View(methods.ToList());
        }

        //
        // GET: /Methods/Details/5

        public ViewResult Details(Guid id)
        {
            Method method = db.Methods.Find(id);
            return View(method);
        }

        //
        // GET: /Methods/Create

        public ActionResult Create()
        {
            ViewBag.IdController = new SelectList(db.SecureControllers, "Id", "Name");
            return View();
        } 

        //
        // POST: /Methods/Create

        [HttpPost]
        public ActionResult Create(Method method)
        {
            if (ModelState.IsValid)
            {
                method.Id = Guid.NewGuid();
                db.Methods.Add(method);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdController = new SelectList(db.SecureControllers, "Id", "Name", method.IdController);
            return View(method);
        }
        
        //
        // GET: /Methods/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Method method = db.Methods.Find(id);
            ViewBag.IdController = new SelectList(db.SecureControllers, "Id", "Name", method.IdController);
            return View(method);
        }

        //
        // POST: /Methods/Edit/5

        [HttpPost]
        public ActionResult Edit(Method method)
        {
            if (ModelState.IsValid)
            {
                db.Entry(method).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdController = new SelectList(db.SecureControllers, "Id", "Name", method.IdController);
            return View(method);
        }

        //
        // GET: /Methods/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Method method = db.Methods.Find(id);
            return View(method);
        }

        //
        // POST: /Methods/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Method method = db.Methods.Find(id);
            db.Methods.Remove(method);
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