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
    /// <summary>
    /// Controlador que se encarga de crear una encuesta, la cual tiene enlazados temas, preguntas y respuestas que la conforman
    /// </summary>
    
    public class SurveysController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        /// <summary>
        /// Muestra el listado de las encuestas existentes
        /// </summary>
        /// <returns>Vista con el listado de encuestas</returns>

        public ViewResult Index()
        {
            return View(db.Surveys.ToList());
        }

        /// <summary>
        /// Permite ver en detalle las encuestas
        /// </summary>
        /// <param name="id">Codigo de la encuesta</param>
        /// <returns>Vista con los detalles de la encuesta</returns>

        public ViewResult Details(Guid id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        /// <summary>
        /// Permite crear una encuesta nueva
        /// </summary>
        /// <returns>Vista para crear la encuesta</returns>

        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        ///  Metodo Httpost de crear una encuesta nueva
        /// </summary>
        /// <param name="survey">Objeto que se crea en el formulario</param>
        /// <returns>Retorna el listado de encuestas si no hay errores, si los hay devuelve la misma vista.</returns>

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

        /// <summary>
        /// Permite editar una encuesta 
        /// </summary>
        /// <param name="id">Codigo de la encuesta</param>
        /// <returns>Retorna la vista para crear una encuesta</returns>
 
        public ActionResult Edit(Guid id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        /// <summary>
        ///  Metodo Httpost de editar encuesta
        /// </summary>
        /// <param name="report">Objeto de tipo de encuesta que trae los elementos de la vista</param>
        /// <returns>Retorna el listado de encuestas si no hay errores, si los hay devuelve la misma vista.</returns>

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

        /// <summary>
        /// Metodo para eliminar una encuesta
        /// </summary>
        /// <param name="id">Codigo de la encuesta</param>
        /// <returns>La vista para eliminar una encuesta</returns>
 
        public ActionResult Delete(Guid id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        /// <summary>
        /// Metodo para confirmar la  eliminacion de una encuesta
        /// </summary>
        /// <param name="id">Codigo de la encuesta</param>
        /// <returns>Retorna el listado de encuestas</returns>

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            TempData["Remove"] = "Se eliminó correctamente la encuesta " + survey.Name + "!";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Método que se ejecuta antes de cargar una encuesta
        /// </summary>
        /// <param name="disposing">Recibe si destruye o no un Objeto</param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}