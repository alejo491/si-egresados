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
    public class LikeController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Like/

        public ViewResult Index()
        {
            return View(db.Likes.ToList());
        }

        //
        // GET: /Like/Details/5

        public ViewResult Details(Guid id)
        {
            Like like = db.Likes.Find(id);
            return View(like);
        }

        //
        // GET: /Like/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Like/Create

        [HttpPost]
        public ActionResult Create(Like like)
        {
            if (ModelState.IsValid)
            {
                like.Id = Guid.NewGuid();
                db.Likes.Add(like);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(like);
        }
        
        //
        // GET: /Like/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Like like = db.Likes.Find(id);
            return View(like);
        }

        //
        // POST: /Like/Edit/5

        [HttpPost]
        public ActionResult Edit(Like like)
        {
            if (ModelState.IsValid)
            {
                db.Entry(like).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(like);
        }

        //
        // GET: /Like/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Like like = db.Likes.Find(id);
            return View(like);
        }

        //
        // POST: /Like/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Like like = db.Likes.Find(id);
            db.Likes.Remove(like);
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