using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
using System.Web.Security;

namespace AplicacionBase.Controllers
{
    /// <summary>
    /// Controlador para la gestión de noticias
    /// </summary>
    public class PostController : Controller
    {
        /// <summary>
        /// Atributo que consulta la base de datos
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();

        #region ListarNoticiasAdministrador
        /// <summary>
        /// Muestra todas las noticias que han publicado los usuarios
        /// </summary>
        /// <returns>Retorna las noticias en el formulario</returns>
        public ViewResult Index()
        {
            var posts = db.Posts.Include(p => p.User);
            return View(posts.ToList());
        }
        #endregion

        #region ListarNoticiasUsuario
        /// <summary>
        /// Muestra todas las noticias que ha publicado el usuario
        /// </summary>
        /// <returns>Retorna las noticias en el formulario</returns>
        public ViewResult Indexpublic()
        {
            var posts = db.Posts.Include(p => p.User);
            return View(posts.ToList());
        }
        #endregion

        #region Detalles
        /// <summary>
        /// Muestra en detalle una noticia
        /// </summary>
        /// <param name="id">Identificador de la noticia</param>
        /// <returns>Retorna el contenido de la noticia para el id correspondiente</returns>
        public ViewResult Details(Guid id)
        {
            Post post = db.Posts.Find(id);
            Guid us = (Guid)Membership.GetUser().ProviderUserKey;
            return View(post);
        }
        #endregion

        #region Crear noticia
        /// <summary>
        /// Permite crear una nueva noticia
        /// </summary>
        /// <returns>Retorna la vista para crear la noticia</returns>
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region Crear noticia HttpPost
        /// <summary>
        /// Guarda la noticia que se recibe en el formulario
        /// </summary>
        /// <param name="post">Noticia recibida desde un formulario</param>
        /// <returns>Retorna las noticias recibidas en el formulario</returns>
        [HttpPost]
        public ActionResult Create(Post post)
        {
            post.IdUser = (Guid)Membership.GetUser().ProviderUserKey;
            post.PublicationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IdUser = new SelectList(db.Users, "Id", "LastNames", post.IdUser);
            return View(post);
        }
        #endregion

        #region Editar noticia
        /// <summary>
        /// Da la opción de editar una noticia que ha sido guardada
        /// </summary>
        /// <param name="id">Identificador de la noticia</param>
        /// <returns>Retorna la noticia a editar</returns>
        public ActionResult Edit(Guid id)
        {
            Post post = db.Posts.Find(id);
            //ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", post.IdUser);
            return View(post);
        }
        #endregion

        #region Editar noticia HttpPost
        /// <summary>
        /// Guarda las modificaciones hechas en una noticia
        /// </summary>
        /// <param name="post">Noticia que se modificó y que se va a actualizar en el formulario</param>
        /// <returns>Retorna la noticia que se editó</returns>
        [HttpPost]
        public ActionResult Edit(Post post)
        {
            post.IdUser = (Guid)Membership.GetUser().ProviderUserKey;
            post.UpdateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", post.IdUser);
            return View(post);
        }
        #endregion

        #region Eliminar noticia
        /// <summary>
        /// Da la opción de eliminar una noticia
        /// </summary>
        /// /// <param name="id">Identificador de la noticia</param>
        /// <returns>Retorna la noticia a eliminar</returns>
        public ActionResult Delete(Guid id)
        {
            Post post = db.Posts.Find(id);
            return View(post);
        }
        #endregion

        #region Eliminar noticia HttpPost
        /// <summary>
        /// Elimina la noticia que corresponde al id
        /// </summary>
        /// <param name="id">Identificador de la noticia</param>
        /// <returns>Retorna el resultado de la eliminación de la noticia</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult ShowPosts()
        {
            var posts = db.Posts.Include(p => p.User);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}