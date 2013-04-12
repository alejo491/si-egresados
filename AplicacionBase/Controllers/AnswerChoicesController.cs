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
        //
        // GET: /AnswerChoices/
        public ActionResult Index(Guid? id)
        {
            if (id != Guid.Empty && id != null)
            {
                 var questions = (Question)db.Questions.Find(id);
                if (questions != null)
                {
                    ViewBag.Question = questions;
                    var AnswerChoices = db.AnswerChoices.Include(o => o.Question).Where(o => o.IdQuestion == id);
                    return View(AnswerChoices.ToList());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (oid != Guid.Empty && oid != null)
            {
                var questions = (Question)db.Questions.Find(oid);
                if (questions != null)
                {
                    ViewBag.Question = questions;                 
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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