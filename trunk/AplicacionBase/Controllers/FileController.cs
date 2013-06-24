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

namespace AplicacionBase.Controllers
{
    /// <summary>
    /// Controlador para la gestión de archivos
    /// </summary>
    public class FileController : Controller
    {
        /// <summary>
        /// Atributo que consulta la base de datos
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();

        #region ListarArchivos
        /// <summary>
        /// Muestra todos los archivos subidos por los usuarios en una noticia
        /// </summary>
        /// <returns>Retorna el archivo en el formulario</returns>
        public ViewResult Index()
        {
            return View(db.Files.ToList());
        }
        #endregion

        #region actualizar archivo
        /// <summary>
        /// Actualiza un archivo que se haya subido para una determinada noticia
        /// </summary>
        /// <param name="id">Identificador del archivo</param>
        /// <returns>Retorna el archivo en el formulario</returns>
        public ViewResult UFile(Guid Id)
        {
            var post = db.Posts.Find(Id);
            return View(post);
        }
        #endregion

        #region Galeria de Archivos Edit
        /// <summary>
        /// Muestra la gaeria de archivos subidos por los usuarios en la vista de Edit
        /// </summary>
        /// <returns>Retorna el archivo en el formulario</returns>
        public ViewResult Galery(Guid Id)
        {
            var files = db.Files.SqlQuery("exec url_file '" + Id + "'");
            return View(files.ToList());
            //return View(db.Files.ToList());
        }
        #endregion

        #region Galeria de archivos para detalles
        /// <summary>
        /// Muestra la galeria de archivos subidos por los usuarios en la vista de view
        /// </summary>
        /// <returns>Retorna el archivo en el formulario</returns>
        public ViewResult GaleryView(Guid Id)
        {
            var files = db.Files.SqlQuery("exec url_file '" + Id + "'");
            return View(files.ToList());
            //return View(db.Files.ToList());
        }
        #endregion

        #region Galeria de Archivos showPost
        
        public ViewResult GaleryPost(Guid Id)
        {
            var files = db.Files.SqlQuery("exec url_file4 '" + Id + "'");
            return View(files.ToList());
        }
        #endregion

        #region Imagen principal 
        /// <summary>
        /// Muestra la gaeria de archivos subidos por los usuarios
        /// </summary>
        /// <returns>Retorna el archivo en el formulario</returns>
        public ViewResult ImageMain(Guid Id)
        {
            var files = db.Files.SqlQuery("exec url_file2 '" + Id + "'");
            return View(files.ToList());
            //return View(db.Files.ToList());
        }
        #endregion

        #region Carrucel de imagenes
        /// <summary>
        /// Muestra la gaeria de archivos subidos por los usuarios
        /// </summary>
        /// <returns>Retorna el archivo en el formulario</returns>
        public ViewResult CarrucelImage(Guid Id)
        {
            var files = db.Files.SqlQuery("exec url_file3 '" + Id + "'");
            return View(files.ToList());           
        }
        #endregion

        #region Detalles
        /// <summary>
        /// Muestra los detalles del archivo
        /// </summary>
        /// <param name="id">Identificador del archivo</param>
        /// <returns>Retorna el archivo para el id correspondiente</returns>
        public ViewResult Details(Guid id)
        {
            AplicacionBase.Models.File file = db.Files.Find(id);
            return View(file);
        }
        #endregion

        #region Crear archivo
        /// <summary>
        /// Permite subir un archivo a una nueva noticia
        /// </summary>
        /// <returns>Retorna la opción para subir el archivo</returns>
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region Crear archivo HttpPost
        /// <summary>
        /// Guarda el archivo recibido en el formulario
        /// </summary>
        /// <param name="file">Archivo rescibido desde el formulario</param>
        /// <returns>Retorna los archivos recibidos en el formulario</returns>
        [HttpPost]
        public ActionResult Create(AplicacionBase.Models.File file)
        {
            if (ModelState.IsValid)
            {
                file.Id = Guid.NewGuid();
                db.Files.Add(file);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(file);
        }
        #endregion

        #region Editar archivo
        /// <summary>
        /// Da la opción cambiar un archivo que haya sido publicado en una noticia
        /// </summary>
        /// <param name="id">Identificador del archivo</param>
        /// <returns>Retorna la noticia a editar</returns>
        public ActionResult Edit(Guid id)
        {
            AplicacionBase.Models.File file = db.Files.Find(id);
            return View(file);
        }
        #endregion

        #region Editar archivo HttpPost
        /// <summary>
        /// Guarda las modificaciones que se hayan hecho de un archivo en una noticia
        /// </summary>
        /// <param name="file">Archivo que se modificó y que se va a actualizar en el formulario</param>
        /// <returns>Retorna el archivo que se modificó</returns>
        [HttpPost]
        public ActionResult Edit(AplicacionBase.Models.File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }
        #endregion

        #region Eliminar archivo
        /// <summary>
        /// Da la opción de eliminar un archivo correspondiente a una noticia
        /// </summary>
        /// /// <param name="id">Identificador del archivo a eliminar</param>
        /// <returns>Retorna el archivo a eliminar</returns>
        public ActionResult Delete(Guid id)
        {
            AplicacionBase.Models.File file = db.Files.Find(id);
            return View(file);
        }
        #endregion

        #region Regresar
        /// <summary>
        /// Da la opción no efectuar cambios en una noticia y regresar al listado principal
        /// </summary>
        /// /// <param name="id">Identificador del archivo cargado</param>
        /// <returns>Retorna el archivo cargado</returns>
        public ActionResult Regresar(Guid id)
        {
            var filepost = db.FilesPosts.SqlQuery("exec relacionfilepost '" + id + "'");
            Guid idpost = filepost.ToList()[0].IdPost;
            return RedirectToAction("Edit", "Post", new { id = idpost });
        }
        #endregion

        #region Eliminar archivo HttpPost
        /// <summary>
        /// Elimina el archivo de la noticia que corresponde al id
        /// </summary>
        /// <param name="id">Identificador de archivo a eliminar</param>
        /// <returns>Retorna el resultado de la eliminación del archivo</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AplicacionBase.Models.File file = db.Files.Find(id);
            var filename = file.Name;

            var filePath = Path.Combine(Server.MapPath("~/UploadFiles"), filename);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            var filepost = db.FilesPosts.SqlQuery("exec relacionfilepost '" + id + "'");
            Guid idpost = filepost.ToList()[0].IdPost;

            db.FilesPosts.Remove(filepost.ToList()[0]);
            db.SaveChanges();
            db.Files.Remove(file);
            db.SaveChanges();

            return RedirectToAction("Edit", "Post", new { id = idpost });
        }
        #endregion

        #region Crear archivo en la noticia
        /// <summary>
        /// Guarda los campos del archivo necesarios en la base de datos y asocia el archivo a la noticia correspondiente.
        /// </summary>
        /// <param name="File">Archivo recibido desde un formulario</param>
        /// <param name="idpost">Id del post donde se va a crear el archivo y que se recibe desde un formulario</param>
        /// <returns>Retorna el archivo en el formulario</returns>
        public void Create2(AplicacionBase.Models.File uploadfile, Guid idpost)
        {
            //if (ModelState.IsValid)
            //{
                FilesPost filepost = new FilesPost();

                filepost.IdPost = idpost;

                if (uploadfile.Type.ToString().Contains("image"))
                {
                    filepost.Main = 1;
                    filepost.Type = uploadfile.Type;
                }
                else
                {
                    filepost.Main = 0;
                    filepost.Type = "Archivo";
                }
                db.Files.Add(uploadfile);
                db.SaveChanges();
                filepost.File = uploadfile;
                db.FilesPosts.Add(filepost);
                db.SaveChanges();
            //}
        }
        #endregion

        #region Descargar archivo
        /// <summary>
        /// Da la opción de descargar un archivo que hay sido subido
        /// </summary>
        /// <param name="id">Identificador del archivo</param>
        /// <returns>Retorna el archivo que se va a descargar en el formulario</returns>
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
        #endregion

        #region subir Archivo de noticias.
        /// <summary>
        /// permite subir al servidor el archivo asociado a una noticia.
        /// </summary>
        /// <param name="post">Objeto del tipo post</param>
        /// <returns>Retorna el archivo en el formulario</returns>
        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpPost]
        public ActionResult UploadFiles(Post post)
        {
            var r = new List<ViewDataUploadFilesResult>();
            var id = post.Id;
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
                        uploadfile.Path = "/UploadFiles/" + obj_file.name.ToString().Replace(" ","_").Replace("-","_"); 
                        uploadfile.Type = obj_file.type;
                        uploadfile.Size = obj_file.size.ToString();
                        Create2(uploadfile, post.Id);
                    }
                }
                JsonResult result = Json(statuses);
                result.ContentType = "text/plain";

                return result;
            }

            return Json(r);
        }
        #endregion

        #region Subida archivo parcial 
        /// <summary>
        /// Permite cargar archivos de gran tamaño.
        /// </summary>
        /// <param name="fileName">Nombre del archivo recibido desde un formulario</param>
        /// <param name="request">Peticion que se hace a la BD para poder actualizar el archivo</param>
        /// <param name="statuses">Muestra el estado del archivo</param>
        /// <returns>Retorna la actualizacion del archivo en el formulario</returns>
        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var fullName = Path.Combine(StorageRoot, Path.GetFileName(fileName));

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
                url = "/Home/Download/" + fileName,
                delete_url = "/Home/Delete/" + fileName,
                thumbnail_url = @"data:image/png;base64," + EncodeFile(fullName),
                delete_type = "GET",
            });
        }
        #endregion

        #region Cargar todo el archivo
        /// <summary>
        /// Carga todo el archivo en una noticia
        /// </summary>
        /// <param name="request">Peticion que se hace a la BD para poder cargar el archivo</param>
        /// <param name="statuses">Muestra el estado de la subida del archivo</param>
        /// <returns>Retorna la actualizacion del archivo en el formulario</returns>
        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];

                var fullPath = Path.Combine(StorageRoot, Path.GetFileName(file.FileName));

                file.SaveAs(fullPath);

                statuses.Add(new ViewDataUploadFilesResult()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = "/Home/Download/" + file.FileName,
                    delete_url = "/Home/Delete/" + file.FileName,
                    thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath),
                    delete_type = "GET",
                });
            }
        }
        #endregion

        #region Raiz de almacenamiento
        /// <summary>
        /// Método que guarda el archivo en la carpeta UploadFiles
        /// </summary>
        private string StorageRoot
        {
            get { return Path.Combine(Server.MapPath("~/UploadFiles")); }
        }
        #endregion

        #region Codificación de los archivos
        /// <summary>
        /// Método que codifica un archivo que se carga
        /// </summary>
        /// <param name="fileName">Archivo a codificar</param>
        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }
        #endregion

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

    #region ViewDataUploadFilesResult
    /// <summary>
    /// Clase para el resultado de los datos del archivo a subir
    /// name: nombre del archivo
    /// size: longitud del archivo
    /// type: formato del archivo
    /// url: enlace para el archivo
    /// delete_url: eliminar enlace del archivo
    /// thumbnail_url: enlace en miniatura del archivo
    /// delete_type: eliminar formato del archivo
    /// </summary>
    public class ViewDataUploadFilesResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string delete_url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_type { get; set; }
    }
    #endregion

}
