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
          //  var questions = db.Questions.Include(q => q.Topic);
            return View(db.Questions.ToList());
        }
        */
        public ActionResult Index(Guid? id)
        {
           
            if (id != Guid.Empty && id != null)
            {

                var topic = (Topic) db.Topics.Find(id);

                if (topic != null)
                {
                    var questionsTopics = db.Questions.Include(s => s.Topic).Where(s => s.IdTopic == id).OrderBy(s=>s.QuestionNumber);
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

            if (id != Guid.Empty && id != null)
            {
                var auxTopic = (Topic)db.Topics.Find(id);

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
        public ActionResult Create(Guid? id, Question question)
        {
          
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid();
                question.IdTopic = new Guid("" + id);
                var topic = (Topic)db.Topics.Find(id); 
                bool existeNumero = ExisteNumero(question.QuestionNumber, question.IdTopic, question.Id);

                if (existeNumero)
                {
                    db.Questions.Add(question);
                    db.SaveChanges();
                    TempData["Success"] = "Se ha creado la pregunta correctamente";
                    var aux = new AnswerChoice();
                    if (question.Type == "Larga" || question.Type == "Corta")
                    {
                        aux.Id = Guid.NewGuid();
                        aux.IdQuestion = question.Id;                       
                        aux.AnswerNumber = 1;
                        aux.NumericValue = 0;
                        aux.Sentence = "Rta";
                        aux.Type = "Normal";
                        db.AnswerChoices.Add(aux);
                        db.SaveChanges();
                        
                    }

                  
                    return RedirectToAction("Index", new {id = id});
               }
                else
                {
                    if (topic != null)
                    {
                        ViewBag.Topic = topic;
                    }
                    TempData["Error2"] = "Este número ya ha sido asignado a otra pregunta";
                    return View();
                }
                
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
 

        public ActionResult Edit(Guid? idT, Guid? idQ)
        {
         
            if (idT != Guid.Empty && idT != null && idQ != Guid.Empty && idQ != null)
            {
                Question question = db.Questions.Find(idQ);
                var auxTopic = db.Topics.Find(idT);

                if (question != null || auxTopic != null)
                {
                    //ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description", question.IdTopic);
                    ViewBag.Topic = auxTopic;
                    return View(question);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
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
                //var question2_ = db.Questions.Find(idQ);
                question.IdTopic = idT;
                //question.Type = question2_.Type;
                
               // question.Id = idQ;
                db.Entry(question).State = EntityState.Modified;

                bool existeNumero = ExisteNumero(question.QuestionNumber, question.IdTopic, question.Id);
                if (!existeNumero)
                {
                    var question_ = db.Questions.Find(idQ);
                    var auxTopic = db.Topics.Find(idT);
                    

                    if (question_ != null || auxTopic != null)
                    {
                        //ViewBag.IdTopic = new SelectList(db.Topics, "Id", "Description", question.IdTopic);
                        ViewBag.Topic = auxTopic;
                        TempData["Error2"] = "Este número ya ha sido asignado a otra pregunta";
                        return View(question);
                    }
                   
                }
                else
                {
                    db.SaveChanges();
                    TempData["Success"] = "Se ha editado la pregunta correctamente";
                    return RedirectToAction("Index", new { id = idT }); 
                }
            }

            return View(question);
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
            TempData["Success"] = "Se ha eliminado la pregunta correctamente";
            return RedirectToAction("Index", new {id=auxTopic.Id});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        /// Metodo que retorna una validacion si el número de pregunta que se le va asignar a la Pregunta es único
        /// </summary>
        /// <param name="questionNumber">Numero de la Pregunta</param>
        /// <param name="IdTopic">Id del Tema</param>
        /// <param name="IdQuestion">Id de la Pregunta</param>
        /// <returns>Verdadero si el numero no existe, falso si el numero ya existe</returns>

        public bool ExisteNumero(Decimal questionNumber, Guid idTopic, Guid IdQuestion)
        {
            //var question = db.Questions.Where(a => a.Id == IdAnswer).
            var q = db.Questions.Where(s => s.IdTopic == idTopic);

            foreach (Question question in q)
            {
                if (question.QuestionNumber == questionNumber && question.Id != IdQuestion)
                {
                    return false;
                }
            }
            return true;
        }

        /*
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
         */
    }
}