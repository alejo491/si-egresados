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
    /// Controlador para la gestión de noticias
    /// </summary>
    public class FilePostController : Controller
    {
        /// <summary>
        /// Atributo que consulta la base de datos
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();

        #region ListarFiles
        /// <summary>
        /// Muestra los archivos que se han subido para una determinada noticia
        /// </summary>
        /// <returns>Retorna el/los archivo(s) subido(s) por cada noticia</returns>
        public ViewResult Index()
        {
            var filesposts = db.FilesPosts.Include(f => f.File).Include(f => f.Post);
            return View(filesposts.ToList());
        }
        #endregion

        #region DetallesFiles
        /// <summary>
        /// Muestra los detalles del archivo para una determinada noticia
        /// </summary>
        /// <param name="id">Identificador del archivo</param>
        /// <returns>Retorna la información de un archivo en una noticia para el id correspondiente</returns>
        public ViewResult Details(Guid id)
        {
            FilesPost filespost = db.FilesPosts.Find(id);
            return View(filespost);
        }
        #endregion


        public ActionResult Create()
        {
            ViewBag.IdFile = new SelectList(db.Files, "Id", "Path");
            ViewBag.IdPost = new SelectList(db.Posts, "Id", "Title");
            return View();
        }


        [HttpPost]
        public ActionResult Create(FilesPost filespost)
        {
            if (ModelState.IsValid)
            {
                db.FilesPosts.Add(filespost);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.IdFile = new SelectList(db.Files, "Id", "Patch", filespost.IdFile);
            ViewBag.IdPost = new SelectList(db.Posts, "Id", "Title", filespost.IdPost);
            return View(filespost);
        }
        

        public ActionResult Edit(Guid id)
        {
            FilesPost filespost = db.FilesPosts.Find(id);
            ViewBag.IdFile = new SelectList(db.Files, "Id", "Patch", filespost.IdFile);
            ViewBag.IdPost = new SelectList(db.Posts, "Id", "Title", filespost.IdPost);
            return View(filespost);
        }


        [HttpPost]
        public ActionResult Edit(FilesPost filespost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filespost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFile = new SelectList(db.Files, "Id", "Patch", filespost.IdFile);
            ViewBag.IdPost = new SelectList(db.Posts, "Id", "Title", filespost.IdPost);
            return View(filespost);
        }


        public ActionResult Delete(Guid id)
        {
            FilesPost filespost = db.FilesPosts.Find(id);
            return View(filespost);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            FilesPost filespost = db.FilesPosts.Find(id);
            db.FilesPosts.Remove(filespost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Método dispose
        /// <summary>
        ///Dispose
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        #endregion

    }
}