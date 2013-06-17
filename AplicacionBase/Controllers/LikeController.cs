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
    /// Controlador para la gestión de los likes de las noticias
    /// </summary
    public class LikeController : Controller
    {
        /// <summary>
        /// Atributo que consulta la base de datos
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();

        #region Likes de las noticias
        /// <summary>
        /// Muestra los likes por cada noticia que han publicado los usuarios
        /// </summary>
        /// <returns>Retorna los likes en el formulario</returns>
        public IList<Like> Index()
        {
            var likes = db.Likes.Include(l => l.Post).Include(l => l.User);
            //return View(likes.ToList());
            return (IList<Like>)likes.ToList();
        }
        #endregion

        #region Detalles
        /// <summary>
        /// Muestra en detalle los likes de una noticia
        /// </summary>
        /// <param name="id">Identificador del like en determinada noticia</param>
        /// <returns>Retorna los likes para una noticia con el id correspondiente</returns>
        public ViewResult Details(Guid id)
        {
            Like like = db.Likes.Find(id);
            return View(like);
        }
        #endregion

        #region Crear un like
        /// <summary>
        /// Permite hacer un like a una noticia
        /// </summary>
        /// <param name="post">Like que se recibe desde un formulario</param>
        /// <returns>Retorna la vista para crear el like de la noticia</returns>
        public ActionResult Create(Guid post)
        {
            Like like = new Like();
            like.Id_Post =post;
            like.Id_User = (Guid)Membership.GetUser().ProviderUserKey;

            if (ModelState.IsValid)
            {
                like.Id = Guid.NewGuid();
                db.Likes.Add(like);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Post", new { id = post});
        }
        #endregion

        #region Editar Like
        /// <summary>
        /// Da la opción de cambiar un Me gusta por un No me gusta ó viceversa
        /// </summary>
        /// <param name="id">Identificador del like</param>
        /// <returns>Retorna el like a editar</returns>
        public ActionResult Edit(Guid id)
        {
            Like like = db.Likes.Find(id);
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", like.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", like.Id_User);
            return View(like);
        }
        #endregion

        #region Editar Like HttpPost
        /// <summary>
        /// Guarda las modificaciones hechas en un like
        /// </summary>
        /// <param name="like">Like que se modificó y que se va a actualizar en el formulario</param>
        /// <returns>Retorna el like que se modificó</returns>
        [HttpPost]
        public ActionResult Edit(Like like)
        {
            if (ModelState.IsValid)
            {
                db.Entry(like).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Post = new SelectList(db.Posts, "Id", "Title", like.Id_Post);
            ViewBag.Id_User = new SelectList(db.Users, "Id", "PhoneNumber", like.Id_User);
            return View(like);
        }
        #endregion

        #region Eliminar Like
        /// <summary>
        /// Da la opción de cambiar el estado del like
        /// </summary>
        /// /// <param name="id">Identificador del like</param>
        /// <returns>Retorna el like que se cambio</returns>
        public ActionResult Delete(Guid id)
        {
            Like like = db.Likes.Find(id);
            return View(like);
        }
        #endregion

        #region Eliminar Like HttpPost
        /// <summary>
        /// Elimina el like que corresponde al id
        /// </summary>
        /// <param name="id">Identificador del like</param>
        /// <param name="post">Identificador de la noticia</param>
        /// <returns>Retorna el resultado del cambio del like</returns>
        //[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid post, Guid id)
        {            
            Like like = db.Likes.Find(id);
            db.Likes.Remove(like);
            db.SaveChanges();

            return RedirectToAction("Details", "Post", new { id = post });
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}