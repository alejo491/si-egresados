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
    public class ItemsController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Items/

        public ViewResult Index()
        {
            var items = db.Items.Include(i => i.Report);
            return View(items.ToList());
        }

        //
        // GET: /Items/Details/5

        public ViewResult Details(Guid id)
        {
            Item item = db.Items.Find(id);
            return View(item);
        }

        //
        // GET: /Items/Create

        public ActionResult Create()
        {
            ViewBag.IdReport = new SelectList(db.Reports, "Id", "Description");
            return View();
        } 

        //
        // POST: /Items/Create

        [HttpPost]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdReport = new SelectList(db.Reports, "Id", "Description", item.IdReport);
            return View(item);
        }
        
        //
        // GET: /Items/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Item item = db.Items.Find(id);
            ViewBag.IdReport = new SelectList(db.Reports, "Id", "Description", item.IdReport);
            return View(item);
        }

        //
        // POST: /Items/Edit/5

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdReport = new SelectList(db.Reports, "Id", "Description", item.IdReport);
            return View(item);
        }

        //
        // GET: /Items/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Item item = db.Items.Find(id);
            return View(item);
        }

        //
        // POST: /Items/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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