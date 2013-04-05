using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;



using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    public class AnswerChoicesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        public static Guid OID;
        //
        // GET: /AnswerChoices/

        public ViewResult Index(Guid? oid)
        {

            if (oid != null) ViewData["oid"] = oid;
            else ViewData["oid"] = OID;
            var AnswerChoices = db.AnswerChoices.Include(o => o.Question);
            return View(AnswerChoices.ToList());
        }
        public ViewResult Details(Guid id)
        {
            AnswerChoice AnswerChoice = db.AnswerChoices.Find(id);

            return View(AnswerChoice);
        }

        //
        // GET: /AnswerChoice/Create

        public ActionResult Create(Guid? oid)
        {
            ViewData["oid"] = oid;

            return View();
        }

        //
        // POST: /AnswerChoice/Create

        [HttpPost]
        public ActionResult Create(AnswerChoice AnswerChoice, Guid? oid)
        {
            AnswerChoice.IdQuestion = new Guid("" + oid);
            if (ModelState.IsValid)
            {
                AnswerChoice.Id = Guid.NewGuid();

                db.AnswerChoices.Add(AnswerChoice);
                db.SaveChanges();
                return RedirectToAction("Index", new { oid = AnswerChoice.IdQuestion });
            }

            // ViewBag.IdQuestion = new SelectList(db.Preguntas, "Id", "Enunciado", AnswerChoice.IdQuestion);
            return View(AnswerChoice);
        }

        //
        // GET: /AnswerChoice/Edit/5

        public ActionResult Edit(Guid id, Guid? oid)
        {

            AnswerChoice AnswerChoice = db.AnswerChoices.Find(id);
            // ViewBag.IdQuestion = new SelectList(db.Preguntas, "Id", "Enunciado", AnswerChoice.IdQuestion);
            return View(AnswerChoice);




        }

        //
        // POST: /AnswerChoice/Edit/5

        [HttpPost]
        public ActionResult Edit(AnswerChoice AnswerChoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(AnswerChoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { oid = AnswerChoice.IdQuestion });
            }
            //ViewBag.IdQuestion = new SelectList(db.Preguntas, "Id", "Enunciado", AnswerChoice.IdQuestion);
            return View(AnswerChoice);
        }

        //
        // GET: /AnswerChoice/Delete/5

        public ActionResult Delete(Guid id)
        {
            AnswerChoice AnswerChoice = db.AnswerChoices.Find(id);
            return View(AnswerChoice);
        }

        //
        // POST: /AnswerChoice/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AnswerChoice AnswerChoice = db.AnswerChoices.Find(id);
            db.AnswerChoices.Remove(AnswerChoice);
            db.SaveChanges();
            return RedirectToAction("Index", new { oid = AnswerChoice.IdQuestion });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}