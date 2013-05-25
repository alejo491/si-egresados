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
    public class StartboxController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Startbox/

        public ViewResult Index()
        {
            var startboxs = db.Startboxs.Include(s => s.Post).Include(s => s.User);
            return View(startboxs.ToList());
        }

        //
        // GET: /Startbox/Details/5

        public ViewResult Details(Guid id)
        {
            Startbox startbox = db.Startboxs.Find(id);
            return View(startbox);
        }

        //
        // GET: /Startbox/Create

        public ActionResult Create()
        {
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title");
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber");
            return View();
        } 

        //
        // POST: /Startbox/Create

        [HttpPost]
        public ActionResult Create(Startbox startbox)
        {
            if (ModelState.IsValid)
            {
                startbox.Id = Guid.NewGuid();
                db.Startboxs.Add(startbox);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", startbox.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", startbox.Id_User);
            return View(startbox);
        }
        
        //
        // GET: /Startbox/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Startbox startbox = db.Startboxs.Find(id);
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", startbox.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", startbox.Id_User);
            return View(startbox);
        }

        //
        // POST: /Startbox/Edit/5

        [HttpPost]
        public ActionResult Edit(Startbox startbox)
        {
            if (ModelState.IsValid)
            {
                db.Entry(startbox).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", startbox.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", startbox.Id_User);
            return View(startbox);
        }

        //
        // GET: /Startbox/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Startbox startbox = db.Startboxs.Find(id);
            return View(startbox);
        }

        //
        // POST: /Startbox/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Startbox startbox = db.Startboxs.Find(id);
            db.Startboxs.Remove(startbox);
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