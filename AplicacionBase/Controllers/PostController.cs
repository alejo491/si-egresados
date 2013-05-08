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
    public class PostController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Post/

        public ViewResult Index()
        {
            var posts = db.Posts.Include(p => p.User);
            return View(posts.ToList());
        }

        //
        // GET: /Post/Details/5

        public ViewResult Details(Guid id)
        {
            Post post = db.Posts.Find(id);
            return View(post);
        }

        //
        // GET: /Post/Create

        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber");
            return View();
        } 

        //
        // POST: /Post/Create

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", post.IdUser);
            return View(post);
        }
        
        //
        // GET: /Post/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Post post = db.Posts.Find(id);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", post.IdUser);
            return View(post);
        }

        //
        // POST: /Post/Edit/5

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", post.IdUser);
            return View(post);
        }

        //
        // GET: /Post/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Post post = db.Posts.Find(id);
            return View(post);
        }

        //
        // POST: /Post/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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