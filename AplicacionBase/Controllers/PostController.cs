using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
using System.Web.Security;
using System.IO;
using PagedList;

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

        /// <summary>
        /// Atributos que permiten controlar la paginación de las vistas
        /// </summary>
        private int pageSize = 10;
        private int pageNumber;

        #region ListarNoticiasGlobal
        /// <summary>
        /// Muestra las noticias autorizadas que han publicado los usuarios
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ViewResult GlobalIndex(int? page)
        {
            var posts = db.Posts.Where(i => i.Autorized == 1);
            pageNumber = (page ?? 1);
            return View(posts.ToList().OrderByDescending(c => c.PublicationDate).ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region ListarNoticiasAdministrador
        /// <summary>
        /// Muestra todas las noticias que han publicado los usuarios
        /// </summary>
        /// <returns>Retorna las noticias en el formulario</returns>
        public ViewResult Index(int? page)
        {
            var posts = db.Posts.Include(p => p.User);
            pageNumber = (page ?? 1);
            var post_basura = db.Posts.SqlQuery("exec PostBasura");
            List<Post> list = post_basura.ToList();
            foreach (var x in list)
            {
                var filepost = db.FilesPosts.SqlQuery("exec relacionfilepost '" + x.Id + "'");
                List<FilesPost> listfp = filepost.ToList();
                foreach (var y in listfp)
                {
                    AplicacionBase.Models.File file = db.Files.Find(y.IdFile);
                    var filename = file.Name;
                    var filePath = Path.Combine(Server.MapPath("~/UploadFiles"), filename);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    db.FilesPosts.Remove(y);
                    db.SaveChanges();
                    db.Files.Remove(file);
                    db.SaveChanges();
                }
                db.Posts.Remove(x);
                db.SaveChanges();
            }
            
            

            return View(posts.ToList().OrderByDescending(c => c.PublicationDate).ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region ListarNoticiasUsuario
        /// <summary>
        /// Muestra todas las noticias que ha publicado el usuario
        /// </summary>
        /// <returns>Retorna las noticias en el formulario</returns>
        public ViewResult UserIndex(int? page)
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {
                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }
            }
            var posts = db.Posts.Where(p => (p.IdUser.Equals(g))).Include(p => p.User);
            pageNumber = (page ?? 1);
            return View(posts.ToList().ToPagedList(pageNumber, pageSize));
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
            Post post2 = new Post();
            IList<Post> datos = new List<Post>();
            datos.Add(post);
            AplicacionBase.Controllers.LikeController lc = new LikeController();
            int cont = 0;
            IList<Like> likes = lc.Index();
            if (Request.IsAuthenticated)
            {
                foreach (Like l in likes)
                {
                    if (l.Id_Post == id && l.Id_User == (Guid)Membership.GetUser().ProviderUserKey)
                    {
                        cont++;
                        post2.Id = l.Id;
                    }
                }
                if (cont == 1) post2.Autorized = 1;
                else post2.Autorized = 0;
            }
            else post2.Autorized = -1;
            datos.Add(post2);
            return View(datos);
        }
        #endregion

        #region Crear noticia
        /// <summary>
        /// Permite crear una nueva noticia
        /// </summary>
        /// <returns>Retorna la vista para crear la noticia</returns>
        public ActionResult Create()
        {
            Post post = new Post();
            post.IdUser = (Guid)Membership.GetUser().ProviderUserKey;
            post.PublicationDate = DateTime.Now;
            post.Autorized = 0; //queda pendiente validar que rol tiene
            post.Main = 0;
            post.Estate = 0;
            post.Id = Guid.NewGuid();
            post.Title = "Remplace Por el Titulo de la Noticia";
            post.Abstract = "Remplace Por el Resumen de la Noticia";
            post.Content = "Remplace Por el Contenido de la Noticia";
            db.Posts.Add(post);
            db.SaveChanges();
            return View(post);
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
            post.Autorized = 0; //queda pendiente validar que rol tiene
            post.Main = 0;
            post.Estate = 1;
            post.PublicationDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
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
            var posts = db.Posts.SqlQuery("exec dbo.Post_getPostMain");
            return View(posts);
        }

        public ActionResult ShowListPosts()
        {
            var posts = db.Posts.SqlQuery("exec dbo.Post_getPostList");
            return View(posts);
        }

        public ActionResult ShowPost(Guid id)
        {            
            //Guid us = (Guid)Membership.GetUser().ProviderUserKey;           
            Post post = db.Posts.Find(id);
            Post post2 = new Post();
            Post post3 = new Post();
            IList<Post> datos = new List<Post>();
            datos.Add(post);
            AplicacionBase.Controllers.LikeController lc = new LikeController();
            AplicacionBase.Controllers.StartboxController st = new StartboxController();
            int cont = 0;
            IList<Like> likes = lc.Index();
            int startbox = st.Index2(id);
            if (Request.IsAuthenticated)
            {
                foreach (Like l in likes)
                {
                    if (l.Id_Post == id && l.Id_User == (Guid)Membership.GetUser().ProviderUserKey)
                    {
                        cont++;
                        post2.Id = l.Id;
                    }
                }
                if (cont == 1) post2.Autorized = 1;
                else post2.Autorized = 0;

            }
            else post2.Autorized = -1;
            post2.Estate = lc.get_likes(id); // guardo en numero de megusta
            post3.Estate = startbox;      //recupero al alificacion
            datos.Add(post2);
            datos.Add(post3);
            return View(datos);
        }

        //! Atiende el resultado de hacer clic en Buscar de la vista Principal
        /*!
         * \param criteria Contiene las palabras clave con las que se desea hacer la busqueda
         * \param page Elemento de control para la paginación
         * \return La vista con el listado de vacantes encontradas para las palabras claves
         *
         */
        public ActionResult Search(string criteria, int? page)
        {
            
            ViewBag.CurrentFilter = criteria;
            if (criteria == null)
            {
                criteria = "";
            }
            string searchText = criteria.ToLower().Trim();
            //Búsqueda
            var posts = db.Posts.Where(p => p.Title.ToLower().Contains(criteria) || p.Abstract.Contains(criteria) ||
               p.Content.Contains(criteria));
            //Ordenar por fecha de publicación
            var results = posts.ToList().OrderByDescending(c => c.PublicationDate);
            pageNumber = (page ?? 1);
            return View(results.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult GlobalSearch(string criteria, int? page)
        {
            ViewBag.CurrentFilter = criteria;
            if (criteria == null)
            {
                criteria = "";
            }
            string searchText = criteria.ToLower().Trim();
            //Búsqueda
            var posts = db.Posts.Where(p => (p.Title.ToLower().Contains(criteria) || p.Abstract.Contains(criteria) ||
               p.Content.Contains(criteria)) 
               && (p.Autorized.Equals(1) && p.Estate.Equals(1)));
            //Ordenar por fecha de publicación
            var results = posts.ToList().OrderByDescending(c => c.PublicationDate);
            pageNumber = (page ?? 1);
            return View(results.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult UserSearch(string criteria, int? page)
        {
            ViewBag.CurrentFilter = criteria;
            if (criteria == null)
            {
                criteria = "";
            }
            string searchText = criteria.ToLower().Trim();
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {
                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }
            }
            //Búsqueda
            var posts = db.Posts.Where(p => (p.Title.ToLower().Contains(criteria) || p.Abstract.Contains(criteria) ||
               p.Content.Contains(criteria)) && (p.IdUser.Equals(g)));
            //Ordenar por fecha de publicación
            var results = posts.ToList().OrderByDescending(c => c.PublicationDate);
            pageNumber = (page ?? 1);
            return View(results.ToPagedList(pageNumber, pageSize));

        }

        [HttpPost]
        public void AutorizarPost(Guid id, int value)
        {
            Post post = db.Posts.Find(id);
            post.Autorized = value;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        [HttpPost]
        public void PostPrincipal(Guid id, int value)
        {
            Post post = db.Posts.Find(id);
            post.Main = value;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        [HttpPost]
        public void CambiarEstadoPost(Guid id, int value)
        {
            Post post = db.Posts.Find(id);
            post.Estate = value;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
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