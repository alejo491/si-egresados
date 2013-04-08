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
    public class SurveysController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Surveys/

        public ViewResult Index()
        {
            return View(db.Surveys.ToList());
        }

        //
        // GET: /Surveys/Details/5

        public ViewResult Details(Guid id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        //
        // GET: /Surveys/Create

        public ActionResult Create()
        {
            return View();
        }

          //
        // POST: /Surveys/Create

        [HttpPost]
        public ActionResult Create(Survey survey)
        {
            if (ModelState.IsValid)
            {
                survey.Id = Guid.NewGuid();
                db.Surveys.Add(survey);
                db.SaveChanges();
                TempData["Sucess"] = "Se registró correctamente la encuesta "+survey.Name+"!";

                return RedirectToAction("Index");  
            }

            return View(survey);
        }
        
        //
        // GET: /Surveys/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        //
        // POST: /Surveys/Edit/5

        [HttpPost]
        public ActionResult Edit(Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Update"] = "Se actualizó correctamente la encuesta " + survey.Name + "!";
                return RedirectToAction("Index");
            }
            return View(survey);
        }

        //
        // GET: /Surveys/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        //
        // POST: /Surveys/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            TempData["Remove"] = "Se eliminó correctamente la encuesta " + survey.Name + "!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}