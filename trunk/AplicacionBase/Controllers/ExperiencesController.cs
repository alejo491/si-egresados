using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
using System.Data;

namespace AplicacionBase.Controllers
{
    public class ExperiencesController : Controller
    {
        //
        // GET: /Experiences/
        private DbSIEPISContext db = new DbSIEPISContext();

        public ActionResult Index()
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {

                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }

            }
            var IdUser = g;
            Guid nulo = System.Guid.Empty;
            if (g != nulo)
            {
                return View(db.Experiences.Where(l => l.IdUser == g));
            }
            else return RedirectPermanent("/Account/LogOn");
        }

        //
        // GET: /Experiences/Details/5

        public ActionResult Details(Guid id)
        {
            Experience experience = db.Experiences.Find(id);
            Boss b = new Boss();
            foreach (var e in db.ExperiencesBosses)
            {
                if (e.IdExperiences == id)
                {
                    b = db.Bosses.Find(e.IdBoss);
                }
            }
            Session["IdBoss"] = b.Id;
            Session["IdExperience"] = id;
            ViewBag.NameBoss = b.Name;
            return View(experience);
        }

        //
        // GET: /Experiences/Create

        public ActionResult Create()
        {
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name");
            ViewBag.IdUser = new SelectList(db.Users, "Id", "Id");
            return View();
        }

        //
        // POST: /Experiences/Create

        [HttpPost]
        public ActionResult Create(Experience experience)
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {

                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }

            }
            var IdUser = g;

            if (ModelState.IsValid)
            {
                experience.Id = Guid.NewGuid();
                experience.IdUser = IdUser;
                db.Experiences.Add(experience);
                db.SaveChanges();
                return RedirectPermanent("/ExperiencesBosses/Create/" + experience.Id);
            }

            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", experience.IdCompanie);
            //ViewBag.IdUser = new SelectList(db.Users, "Id", "Id", vacancy.IdUser);
            return View(experience);
        }

        //
        // GET: /Experiences/Edit/5

        public ActionResult Edit(Guid id)
        {
            Experience experience = db.Experiences.Find(id);
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", experience.IdCompanie);
            // ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);
            return View(experience);
        }

        //
        // POST: /Experiences/Edit/5

        [HttpPost]
        public ActionResult Edit(Experience experience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", experience.IdCompanie);
            //   ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);
            return View(experience);
        }

        //
        // GET: /Experiences/Delete/5

        public ActionResult Delete(Guid id)
        {
            Experience experience = db.Experiences.Find(id);
            return View(experience);
        }

        //
        // POST: /Vacancies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Experience experience = db.Experiences.Find(id);
            ExperiencesBoss EB = new ExperiencesBoss();
            foreach (var e in db.ExperiencesBosses)
            {
                if (e.IdExperiences == id)
                {
                    EB = db.ExperiencesBosses.Find(e.Id);
                }
            }
            if(EB!=null) db.ExperiencesBosses.Remove(EB);
            db.Experiences.Remove(experience);

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
