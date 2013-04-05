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
    public class QuestionsController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Questions/

        public ViewResult Index()
        {
            var questions = db.Questions.Include(q => q.Topic);
            return View(questions.ToList());
        }

        //
        // GET: /Questions/Details/5

        public ViewResult Details(Guid id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // GET: /Questions/Create

        public ActionResult Create()
        {
            ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description");
            return View();
        } 

        //
        // POST: /Questions/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid();
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description", question.IdTopic);
            return View(question);
        }
        
        //
        // GET: /Questions/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Question question = db.Questions.Find(id);
            ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description", question.IdTopic);
            return View(question);
        }

        //
        // POST: /Questions/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description", question.IdTopic);
            return View(question);
        }

        //
        // GET: /Questions/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // POST: /Questions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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