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
    public class FilePostController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /FilePost/

        public ViewResult Index()
        {
            var filesposts = db.FilesPosts.Include(f => f.File).Include(f => f.Post);
            return View(filesposts.ToList());
        }

        //
        // GET: /FilePost/Details/5

        public ViewResult Details(Guid id)
        {
            FilesPost filespost = db.FilesPosts.Find(id);
            return View(filespost);
        }

        //
        // GET: /FilePost/Create

        public ActionResult Create()
        {
            ViewBag.IdFile = new SelectList(db.Files, "Id", "Patch");
            ViewBag.IdPost = new SelectList(db.Posts, "Id", "Title");
            return View();
        } 

        //
        // POST: /FilePost/Create

        [HttpPost]
        public ActionResult Create(FilesPost filespost)
        {
            if (ModelState.IsValid)
            {
                db.FilesPosts.Add(filespost);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdFile = new SelectList(db.Files, "Id", "Patch", filespost.IdFile);
            ViewBag.IdPost = new SelectList(db.Posts, "Id", "Title", filespost.IdPost);
            return View(filespost);
        }
        
        //
        // GET: /FilePost/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            FilesPost filespost = db.FilesPosts.Find(id);
            ViewBag.IdFile = new SelectList(db.Files, "Id", "Patch", filespost.IdFile);
            ViewBag.IdPost = new SelectList(db.Posts, "Id", "Title", filespost.IdPost);
            return View(filespost);
        }

        //
        // POST: /FilePost/Edit/5

        [HttpPost]
        public ActionResult Edit(FilesPost filespost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filespost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFile = new SelectList(db.Files, "Id", "Patch", filespost.IdFile);
            ViewBag.IdPost = new SelectList(db.Posts, "Id", "Title", filespost.IdPost);
            return View(filespost);
        }

        //
        // GET: /FilePost/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            FilesPost filespost = db.FilesPosts.Find(id);
            return View(filespost);
        }

        //
        // POST: /FilePost/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            FilesPost filespost = db.FilesPosts.Find(id);
            db.FilesPosts.Remove(filespost);
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