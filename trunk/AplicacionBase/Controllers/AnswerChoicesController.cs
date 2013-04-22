﻿using System;
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
        public ActionResult Create(Guid? id)
        {
            if (id != Guid.Empty && id != null)
            {
                var questions = (Question)db.Questions.Find(id);
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
        public ActionResult Create(Guid? id, AnswerChoice answerChoice)
        {
            if (ModelState.IsValid)
            {
                
                answerChoice.Id = Guid.NewGuid();
                answerChoice.IdQuestion = new Guid(""+id);
                var questions = (Question)db.Questions.Find(id);                
                bool existeNumero = ExisteNumero(answerChoice.AnswerNumber, answerChoice.IdQuestion, answerChoice.Id);
				if(existeNumero)
				{
                    db.AnswerChoices.Add(answerChoice);
                    db.SaveChanges();
                    TempData["Success"] = "Se ha creado la Opción de Respuesta correctamente";
                    return RedirectToAction("Index", new { id = answerChoice.IdQuestion });
				}
				else
				{
				    if (questions != null)
				    {
				        ViewBag.Question = questions;
				    }
				    TempData["Error2"] = "Este número ya ha sido asignado a otra opción de respuesta";
					return View();
				}
			}
            // ViewBag.IdQuestion = new SelectList(db.Preguntas, "Id", "Enunciado", AnswerChoice.IdQuestion);
            return View(answerChoice);			
        }


        //
        // GET: /AnswerChoice/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id != Guid.Empty && id != null )
            {
                 AnswerChoice answerChoice = db.AnswerChoices.Find(id);
                 var achoice = db.AnswerChoices.Find(id);
                 if(achoice != null)
                 {
                     return View(answerChoice);
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
        // POST: /AnswerChoice/Edit/5
        [HttpPost]
        public ActionResult Edit(AnswerChoice answerChoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answerChoice).State = EntityState.Modified;
                
                bool existeNumero = ExisteNumero(answerChoice.AnswerNumber, answerChoice.IdQuestion, answerChoice.Id);
                if (!existeNumero)
                {
                    var questions = (Question)db.Questions.Find(answerChoice.IdQuestion);
                    if (questions != null)
                    {
                        ViewBag.Question = questions;
                    }
                    TempData["Error2"] = "Este número ya ha sido asignado a otra opción de respuesta";
                    return View(answerChoice);
                }
                else
                {
                    
                    db.SaveChanges();
                    TempData["Success"] = "Se ha editado la Opción de Respuesta correctamente";
                    return RedirectToAction("Index", new { id = answerChoice.IdQuestion });
                
                }


            }
            //ViewBag.IdQuestion = new SelectList(db.Preguntas, "Id", "Enunciado", AnswerChoice.IdQuestion);
            return View(answerChoice);
        }


        //
        // GET: /AnswerChoice/Delete/5
        public ActionResult Delete(Guid id)
        {

            if (id != Guid.Empty && id != null )
            {
                 AnswerChoice answerChoice = db.AnswerChoices.Find(id);
                 var achoice = db.AnswerChoices.Find(id);
                 if(achoice != null)
                 {
                     return View(answerChoice);
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
        // POST: /AnswerChoice/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AnswerChoice AnswerChoice = db.AnswerChoices.Find(id);
            db.AnswerChoices.Remove(AnswerChoice);
            db.SaveChanges();
			TempData["Success"] = "Se ha eliminado la Opción de respuesta correctamente";
            return RedirectToAction("Index", new { id = AnswerChoice.IdQuestion });
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
		
        /// Metodo que retorna una validacion si el número de orden que se le va asignar a la opcion de respuesta es único
        /// </summary>
        /// <param name="TopicNumber">Numero de la Opción de respuesta</param>
        /// <returns>Verdadero si el numero no existe, falso si el numero ya existe</returns>

        public bool ExisteNumero(Decimal TopicNumber, Guid IdQuestion, Guid IdChoice)
        {
            //var question = db.Questions.Where(a => a.Id == IdAnswer).
            var answer = db.AnswerChoices.Where(s => s.IdQuestion == IdQuestion);

            
            foreach (AnswerChoice answerChoice in answer)
            {
                if (answerChoice.AnswerNumber == TopicNumber && answerChoice.Id != IdChoice)
                {
                    return false;
                }
            }
            return true;
        }
    }
}