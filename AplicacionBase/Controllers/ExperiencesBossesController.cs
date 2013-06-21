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
    public class ExperiencesBossesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /ExperiencesBosses/

        public ViewResult Index()
        {
            var experiencesbosses = db.ExperiencesBosses.Include(e => e.Boss).Include(e => e.Experience);
            return View(experiencesbosses.ToList());
        }

        //
        // GET: /ExperiencesBosses/Details/5

        public ViewResult Details(Guid id)
        {
            ExperiencesBoss experiencesboss = db.ExperiencesBosses.Find(id);
            return View(experiencesboss);
        }

        //
        // GET: /ExperiencesBosses/Create

        public ActionResult Create(Guid id, int wizardStep = 0)
        {
            ViewBag.WizardStep = wizardStep;
            ViewBag.IdBoss = new SelectList(db.Bosses, "Id", "Name");
            ViewBag.IdExperiences = id;
            return View();
        }


        //
        // POST: /ExperiencesBosses/Create

        [HttpPost]
        public ActionResult Create(ExperiencesBoss experiencesboss)
        {
            if (ModelState.IsValid)
            {
                experiencesboss.Id = Guid.NewGuid();
                db.ExperiencesBosses.Add(experiencesboss);
                db.SaveChanges();
                return RedirectPermanent("/Experiences/index?wizardStep=1");
            }

            ViewBag.IdBoss = new SelectList(db.Bosses, "Id", "Name", experiencesboss.IdBoss);
            ViewBag.IdExperiences = experiencesboss.IdExperiences;
            return View(experiencesboss);
        }

        //
        // GET: /ExperiencesBosses/Edit/5

        public ActionResult Edit(Guid id)
        {
            ExperiencesBoss experiencesboss = db.ExperiencesBosses.Find(id);
            ViewBag.IdBoss = new SelectList(db.Bosses, "Id", "Name", experiencesboss.IdBoss);
            ViewBag.IdExperiences = new SelectList(db.Experiences, "Id", "Charge", experiencesboss.IdExperiences);
            return View(experiencesboss);
        }

        //
        // POST: /ExperiencesBosses/Edit/5

        [HttpPost]
        public ActionResult Edit(ExperiencesBoss experiencesboss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experiencesboss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBoss = new SelectList(db.Bosses, "Id", "Name", experiencesboss.IdBoss);
            ViewBag.IdExperiences = new SelectList(db.Experiences, "Id", "Charge", experiencesboss.IdExperiences);
            return View(experiencesboss);
        }

        //
        // GET: /ExperiencesBosses/Delete/5

        public ActionResult Delete(Guid id)
        {
            ExperiencesBoss experiencesboss = db.ExperiencesBosses.Find(id);
            return View(experiencesboss);
        }

        //
        // POST: /ExperiencesBosses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ExperiencesBoss experiencesboss = db.ExperiencesBosses.Find(id);
            db.ExperiencesBosses.Remove(experiencesboss);
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