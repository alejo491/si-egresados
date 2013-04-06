using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    public class UserController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /User/

        public ViewResult Index()
        {
            var users = db.Users.Include(u => u.aspnet_Users);
            return View(users.ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(Guid id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.UserName = HttpContext.User.Identity.Name;
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /User/Create
        public Guid buscarId()
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {

                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }

            }
            return g;
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            Guid g = buscarId();
            if (ModelState.IsValid && !g.Equals(System.Guid.Empty))
            {
                //string nombre = HttpContext.cu .User.Identity

                user.Id = g;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit2()
        {
            Guid g = buscarId();
            ViewBag.UserName = HttpContext.User.Identity.Name;
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName");
            User user = db.Users.Find(g);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit2(User user)
        {
            if (ModelState.IsValid)
            {
                Guid g = buscarId();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }

        public ActionResult Edit(Guid id)
        {
            User user = db.Users.Find(id);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }


        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                Guid g = buscarId();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(Guid id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
