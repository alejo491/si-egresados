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
    public class FileController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /File/

        public ViewResult Index()
        {
            return View(db.Files.ToList());
        }
        public ViewResult UploadFile(Guid Id)
        {
            return View();
        }

        public ViewResult Galery(Guid Id)
        {
            var files = db.Files.SqlQuery("exec url_file '" + Id + "'");
            return View(files.ToList());
            //return View(db.Files.ToList());
        }
        //
        // GET: /File/Details/5

        public ViewResult Details(Guid id)
        {
            AplicacionBase.Models.File file = db.Files.Find(id);
            return View(file);
        }

        //
        // GET: /File/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /File/Create

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
        
        //
        // GET: /File/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            AplicacionBase.Models.File file = db.Files.Find(id);
            return View(file);
        }

        //
        // POST: /File/Edit/5

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

        //
        // GET: /File/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            AplicacionBase.Models.File file = db.Files.Find(id);
            return View(file);
        }

        //
        // POST: /File/Delete/5

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

            db.FilesPosts.Remove(filepost.ToList()[0]);
            db.SaveChanges();
            db.Files.Remove(file);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /////////////////////////////////////////////////////////////////////////////////////

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
        public ActionResult UploadFiles(Guid Id)
        {
            var r = new List<ViewDataUploadFilesResult>();

            Guid id_pos = Id;
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
                        Create2(uploadfile, id_pos);
                        
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
   
}