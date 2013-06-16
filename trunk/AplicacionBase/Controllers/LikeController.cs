using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
using System.Web.Security;

namespace AplicacionBase.Controllers
{ 
    public class LikeController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Like/

        public IList<Like> Index()
        {
            var likes = db.Likes.Include(l => l.Post).Include(l => l.User);
            //return View(likes.ToList());
            return (IList<Like>)likes.ToList();
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

        public ActionResult Create(Guid post)
        {
            Like like = new Like();
            like.Id_Post =post;
            like.Id_User = (Guid)Membership.GetUser().ProviderUserKey;

            if (ModelState.IsValid)
            {
                like.Id = Guid.NewGuid();
                db.Likes.Add(like);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Post", new { id = post});
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

        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid post, Guid id)
        {            
            Like like = db.Likes.Find(id);
            db.Likes.Remove(like);
            db.SaveChanges();

            return RedirectToAction("Details", "Post", new { id = post });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}