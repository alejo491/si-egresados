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

        #region ListarNoticiasAdministrador
        /// <summary>
        /// Muestra todas las noticias que han publicado los usuarios
        /// </summary>
        /// <returns>Retorna las noticias en el formulario</returns>
        public ViewResult Index(int? page)
        {
            var posts = db.Posts.Include(p => p.User);
            pageNumber = (page ?? 1);
            return View(posts.ToList().OrderByDescending(c => c.PublicationDate).ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region ListarNoticiasUsuario
        /// <summary>
        /// Muestra todas las noticias que ha publicado el usuario
        /// </summary>
        /// <returns>Retorna las noticias en el formulario</returns>
        public ViewResult Indexpublic(int? page)
        {
            var posts = db.Posts.Include(p => p.User);
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
            Post post2 =new Post();
            IList<Post> datos = new List<Post>();
            datos.Add(post);

            AplicacionBase.Controllers.LikeController lc = new LikeController();
            int cont = 0;
            
            IList<Like> likes = lc.Index();
            //bool b= (Guid)Membership.GetUser().ProviderUserKey;

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
            post.Autorized = 0; //queda pendiente validar que rol tiene
            post.Main = 0;
            post.Estate = 1;

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
            //var posts = db.Posts.SqlQuery("exec dbo.Post_getPostPrincipales");
            return View(posts);
        }

		public ActionResult ShowPost(Guid id)
        {
            //Post post = db.Posts.Find(id);
            //Guid us = (Guid)Membership.GetUser().ProviderUserKey;
            //return View(post);

            Post post = db.Posts.Find(id);
            Post post2 = new Post();
            Post post3 = new Post();

            IList<Post> datos = new List<Post>();
            datos.Add(post);

            AplicacionBase.Controllers.LikeController lc = new LikeController();
            AplicacionBase.Controllers.StartboxController st = new StartboxController();
            int cont = 0;

            IList<Like> likes = lc.Index();
            IList<Startbox> startbox = st.Index();

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
                else post2.Autorized= 0;

            }
            else post2.Autorized = -1;
            post2.Estate = lc.get_likes(id); // guardo en numero de megusta
            datos.Add(post2);
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

///////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public ActionResult Create2(AplicacionBase.Models.File uploadfile,Guid idpost)
        {
            if (ModelState.IsValid)
            {
                FilesPost filepost = new FilesPost();

                filepost.IdPost = idpost;
                if (uploadfile.Type.ToString().Contains("image"))
                {
                    filepost.Main = 1;
                    filepost.Type = uploadfile.Type;
                }
                filepost.Main = 0;
                filepost.Type = uploadfile.Type;
                filepost.File = uploadfile;
                db.FilesPosts.Add(filepost);
                db.SaveChanges();
            }

            return View();
        }

        private string StorageRoot
        {
            get { return Path.Combine(Server.MapPath("~/UploadFiles")); }
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}


        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //[HttpGet]
        //public void Delete(string id)
        //{
        //    var filename = id;

        //    var filePath = Path.Combine(Server.MapPath("~/Files"), filename);

        //    if (System.IO.File.Exists(filePath))
        //    {
        //        System.IO.File.Delete(filePath);
        //    }
        //}

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Download(string id)
        {
            var filename = id;
            var filePath = Path.Combine(Server.MapPath("~/UploadFiles"), filename);

            var context = HttpContext;

            if (System.IO.File.Exists(filePath))
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                context.Response.ContentType = "application/octet-stream";
                context.Response.ClearContent();
                context.Response.WriteFile(filePath);
            }
            else
                context.Response.StatusCode = 404;
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //[HttpPost]
        //public ActionResult UploadFiles2(FormCollection form)
        //{

        //}
      
        
        [HttpPost]
        public ActionResult UploadFiles(Post post)
        {
            var r = new List<ViewDataUploadFilesResult>();
            Post post_temp = new Post();

           
            var IdUser = (Guid)Membership.GetUser().ProviderUserKey;

            
            post_temp.Title = post.Title;
            post_temp.Abstract = post.Abstract;
            post_temp.Content = post.Content;
            post_temp.Autorized = 0; //queda pendiente validar que rol tiene
            post_temp.Main = 0;
            post_temp.Estate = 1;

            Guid id_post = System.Guid.Empty;

            foreach (var p in db.Posts)
            {
                if (p.Title.Equals(post_temp.Title) && p.Abstract.Equals(post_temp.Abstract))
                {
                    id_post = p.Id;
                }
            }

            if (id_post == System.Guid.Empty)
            {
                post_temp.Id = Guid.NewGuid();
                id_post = post_temp.Id;
                post_temp.IdUser = IdUser;
                post_temp.PublicationDate = DateTime.Now;
                db.Posts.Add(post_temp);
                db.SaveChanges();
            }

            foreach (string file in Request.Files)
            {
                var statuses = new List<ViewDataUploadFilesResult>();
                var headers = Request.Headers;

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(Request, statuses);
                }
                else
                {
                    UploadPartialFile(headers["X-File-Name"], Request, statuses);
                }
                ViewDataUploadFilesResult obj_file = new ViewDataUploadFilesResult();

                for (int i = 0; i < statuses.Count; i++)
                {
                    obj_file = statuses[i];//    if (ModelState.IsValid)
                    {
                        AplicacionBase.Models.File uploadfile = new AplicacionBase.Models.File();
                        uploadfile.Id = Guid.NewGuid();
                        uploadfile.Name = obj_file.name;
                        uploadfile.Path = "/UploadFiles/"+obj_file.name.ToString();
                        uploadfile.Type = obj_file.type;
                        uploadfile.Size = obj_file.size.ToString();
                        db.Files.Add(uploadfile);
                        db.SaveChanges();
                        Create2(uploadfile, id_post);
                        
                    }
                }

                JsonResult result = Json(statuses);
                result.ContentType = "text/plain";
                return result;
            }

            return Json(r);
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;
            var fullName = Path.Combine(StorageRoot,  Path.GetFileName(fileName));

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }

                fs.Flush();
                fs.Close();
            }

            statuses.Add(new ViewDataUploadFilesResult()
            {
                name = fileName,
                size = file.ContentLength,
                type = file.ContentType,
                url = "/Post/Download/" + fileName,
                url_fullName = fullName,
                delete_url = "/Post/Delete/" + fileName,
                thumbnail_url = @"data:image/png;base64," + EncodeFile(fullName),
                delete_type = "GET",
            });
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                var fullPath = Path.Combine(StorageRoot,Path.GetFileName(file.FileName));

                file.SaveAs(fullPath);


                statuses.Add(new ViewDataUploadFilesResult()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = "/Post/Download/" + file.FileName,
                    delete_url = "/Post/Delete/" + file.FileName,
                    url_fullName = fullPath,
                    thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath),
                    delete_type = "GET",
                });
            }
        }
    }

    public class ViewDataUploadFilesResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string delete_url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_type { get; set; }
        public string url_fullName { get; set; }
    }
}