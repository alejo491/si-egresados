﻿using System;
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
    /// Controlador para la gestión de calificación
    /// </summary>
    public class StartboxController : Controller
    {
        /// <summary>
        /// Atributo que consulta la base de datos
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();

        #region Calificación de las noticias
        /// <summary>
        /// Muestra la calificación por cada noticia que han publicado los usuarios
        /// </summary>
        /// <returns>Retorna la calificación en el formulario</returns>
        public IList<Startbox> Index()
        {
            var startboxs = db.Startboxs.Include(s => s.Post).Include(s => s.User);
            return (IList<Startbox>)startboxs.ToList();
        }
        #endregion

        #region Calificación de las noticias
        /// <summary>
        /// Muestra la calificación por cada noticia que han publicado los usuarios
        /// </summary>
        /// <param name="id">Identificador de la calificación</param>
        /// <returns>Retorna la calificación en el formulario</returns>
        public int Index2(Guid id)
        {
            var startboxs = db.Startboxs.SqlQuery("exec dbo.get_promedio_post '" + id + "'");
            IList<Startbox> list = (IList<Startbox>)startboxs.ToList();
            int suma = 0;
            foreach (Startbox l in list)
                suma = suma+l.Qualification;
            if (list.Count == 0) return 0;
            else return (suma / list.Count);
        }
        #endregion

        #region Detalles
        /// <summary>
        /// Muestra en detalle la calificación de una noticia
        /// </summary>
        /// <param name="id">Identificador de la calificación de una determinada noticia</param>
        /// <returns>Retorna la calificación para una noticia con el id correspondiente</returns>
        public ViewResult Details(Guid id)
        {
            Startbox startbox = db.Startboxs.Find(id);
            return View(startbox);
        }
        #endregion

        #region Crear calificación
        /// <summary>
        /// Permite hacer dar una calificación a una determinada noticia
        /// </summary>
        /// <returns>Retorna la vista para crear la calificación de la noticia</returns>
        public ActionResult Create()
        {
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title");
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber");
            return View();
        }
        #endregion

        #region Crear calificación HttpPost
        /// <summary>
        /// Guarda la calificación que se recibe en el formulario
        /// </summary>
        /// <param name="startbox">Calificación recibida desde un formulario</param>
        /// <returns>Retorna la calificación en el formulario</returns>
        [HttpPost]
        public ActionResult Create(Startbox startbox)
        {
            if (ModelState.IsValid)
            {
                startbox.Id = Guid.NewGuid();
                db.Startboxs.Add(startbox);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", startbox.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", startbox.Id_User);
            return View(startbox);
        }
        #endregion

        #region Editar calificación
        /// <summary>
        /// Da la opción de cambiar la calificación, ya sea con mayor o menor calificación
        /// </summary>
        /// <param name="id">Identificador de la calificación</param>
        /// <returns>Retorna la calificación a editar</returns>
        public ActionResult Edit(Guid id)
        {
            Startbox startbox = db.Startboxs.Find(id);
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", startbox.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", startbox.Id_User);
            return View(startbox);
        }
        #endregion

        #region Editar calificación HttpPost
        /// <summary>
        /// Guarda las modificaciones hechas en una calificación
        /// </summary>
        /// <param name="startbox">calificación que se modificó y que se va a actualizar en el formulario</param>
        /// <returns>Retorna la calificación que se modificó</returns>
        [HttpPost]
        public ActionResult Edit(Startbox startbox)
        {
            if (ModelState.IsValid)
            {
                db.Entry(startbox).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", startbox.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", startbox.Id_User);
            return View(startbox);
        }
        #endregion

        #region Eliminar calificación
        /// <summary>
        /// Da la opción de cambiar el estado de la calificación
        /// </summary>
        /// /// <param name="id">Identificador de la calificación</param>
        /// <returns>Retorna la calificación que se cambio</returns>
        public ActionResult Delete(Guid id)
        {
            Startbox startbox = db.Startboxs.Find(id);
            return View(startbox);
        }
        #endregion

        #region Eliminar calificación HttpPost
        /// <summary>
        /// Elimina la calificación que corresponde al id
        /// </summary>
        /// <param name="id">Identificador de la calificación</param>
        /// <returns>Retorna el resultado del cambio de la calificación</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Startbox startbox = db.Startboxs.Find(id);
            db.Startboxs.Remove(startbox);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}