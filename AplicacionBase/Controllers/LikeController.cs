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
            var likes = db.Likes.Include(l => l.Post).Include(l => l.User);
            return View(likes.ToList());
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
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title");
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber");
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

            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", like.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", like.Id_User);
            return View(like);
        }
        
        //
        // GET: /Like/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Like like = db.Likes.Find(id);
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", like.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", like.Id_User);
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
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", like.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", like.Id_User);
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