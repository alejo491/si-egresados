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
        /*
        public ViewResult Index()
        {
            var questions = db.Questions.Include(q => q.Topic);
            return View(questions.ToList());
        }
        */


        public ActionResult Index(Guid? id)
        {
            if (id != Guid.Empty && id != null)
            {

                var topic = db.Topics.Find(id);

                if (topic != null)
                {
                    var questionsTopics = db.Questions.Include(s => s.Topic).Where(s => s.IdTopic == id);
                    ViewBag.Topic = topic;
                    return View(questionsTopics.ToList());
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
        // GET: /Questions/Details/5
        


        public ViewResult Details(Guid id)
        {
            Question question = db.Questions.Find(id);
            var auxTopic = db.Topics.Find(question.IdTopic);
            //ViewBag.Questions = question;
            ViewBag.Topic = auxTopic;
            return View(question);
        }

        //
        // GET: /Questions/Create

        public ActionResult Create(Guid? id)
        {
            var auxTopic = db.Topics.Find(id);
            
            if (auxTopic != null)
            {
                ViewBag.Topic = auxTopic;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");                     
            }
        }

        /*
        public ActionResult Create()
        {
            ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description");
            return View();
        } 
        */
        //
        // POST: /Questions/Create

        [HttpPost]
        public ActionResult Create(Guid id, Question question)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid();
                question.IdTopic = id;
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index", new {id=id});
            }

           // ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description", question.IdTopic);
            return View(question);
        }

        /*
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
        */
        //
        // GET: /Questions/Edit/5
 

        public ActionResult Edit(Guid idT, Guid idQ)
        {
            Question question = db.Questions.Find(idQ);
            var auxTopic = db.Topics.Find(idT);

            if (question != null || auxTopic != null)
            {
                //ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description", question.IdTopic);
                ViewBag.Topic = auxTopic;
                return View(question);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Questions/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid idT, Guid idQ, Question question)
        {
            if (ModelState.IsValid)
            {
                question.IdTopic = idT;
               // question.Id = idQ;
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = idT });
            }

            Question question_ = db.Questions.Find(idQ);
            var auxTopic = db.Topics.Find(idT);

            if (question_ != null || auxTopic != null)
            {
                //ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description", question.IdTopic);
                ViewBag.Topic = auxTopic;
                return View(question_);
            }
            return RedirectToAction("Index", "Home");
        }


        /*
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
        */
        //
        // GET: /Questions/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Question question = db.Questions.Find(id);
            if (question != null)
            {
                var auxTopic = db.Topics.Find(question.IdTopic);
                ViewBag.Topic = auxTopic; 
                return View(question);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Questions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Question question = db.Questions.Find(id);
            var auxTopic = db.Topics.Find(question.IdTopic);
            ViewBag.Topic = auxTopic;
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index", new {id=auxTopic.Id});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult ExisteNumero(Decimal QuestionNumber)
        {
            var questions = db.Questions;
            foreach (Question q in questions)
            {
                if (q.QuestionNumber == QuestionNumber)
                {
                    return Json(false);
                }
            }
            return Json(true);
        }
    }
}