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
    public class Aspnet_RolesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        #region Listar Roles
        //
        // GET: /Aspnet_Roles/
        /// <summary>
        /// Muestra los Roles existentes y da la opcion de modificarles los permisos
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            var aspnet_roles = db.aspnet_Roles.Include(a => a.aspnet_Applications);
            return View(aspnet_roles.ToList());
        }
        #endregion
        #region Detalles
        //
        // GET: /Aspnet_Roles/Details/5
        /// <summary>
        /// Muesta en detalle los Roles
        /// </summary>
        /// <param name="id">Identificador del Rol</param>
        /// <returns></returns>
        public ViewResult Details(Guid id)
        {
            aspnet_Roles aspnet_roles = db.aspnet_Roles.Find(id);
            return View(aspnet_roles);
        }
        #endregion
        #region Crear Rol
        //
        // GET: /Aspnet_Roles/Create
        /// <summary>
        /// Opcion para crear u nuevo Rol
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            // ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName");
            return View();
        }
        #endregion
        #region Crear Rol HTTPPost
        //
        // POST: /Aspnet_Roles/Create
        /// <summary>
        /// Guarda un Rol recibido desde URL
        /// </summary>
        /// <param name="aspnet_roles">Recibe un Rol para ser guardado</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(aspnet_Roles aspnet_roles)
        {
            if (ModelState.IsValid)
            {
                aspnet_roles.ApplicationId = db.aspnet_Applications.First().ApplicationId;
                aspnet_roles.RoleId = Guid.NewGuid();
                aspnet_roles.LoweredRoleName = "" + aspnet_roles.RoleName.ToLower();
                db.aspnet_Roles.Add(aspnet_roles);
                db.SaveChanges();
                TempData["Success"] = "Se ha creado el rol " + aspnet_roles.RoleName + " correctamente";
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_roles.ApplicationId);
            return View(aspnet_roles);
        }
        #endregion
        #region Editar Roles
        //
        // GET: /Aspnet_Roles/Edit/5
        /// <summary>
        /// Da la opcion para editar un Rol
        /// </summary>
        /// <param name="id">Identifiacador del Rol a Editar</param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            /* aspnet_Roles aspnet_roles = db.aspnet_Roles.Find(id);
             ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_roles.ApplicationId);
             return View(aspnet_roles);*/
            if (id != Guid.Empty && id != null)
            {
                aspnet_Roles aspnetroles = db.aspnet_Roles.Find(id);
                var achoice = db.aspnet_Roles.Find(id);
                if (achoice != null)
                {
                    return View(aspnetroles);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
        #region Editartar Roles HttpPost
        //
        // POST: /Aspnet_Roles/Edit/5
        /// <summary>
        /// Guarda las modificaciones del Rol Que se recibe mediante un formulario
        /// </summary>
        /// <param name="aspnet_roles">Rol recibido par guardar los cambios</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(aspnet_Roles aspnet_roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspnet_roles).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Se ha Actualizado el rol " + aspnet_roles.RoleName + " correctamente";
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.aspnet_Applications, "ApplicationId", "ApplicationName", aspnet_roles.ApplicationId);
            return View(aspnet_roles);
        }
        #endregion
        #region Eliminar Roles
        //
        // GET: /Aspnet_Roles/Delete/5
        /// <summary>
        /// Da la opcionm de eliminar un Rol
        /// </summary>
        /// <param name="id">Identificador del Rol a Eliminar</param>
        /// <returns></returns>
        public ActionResult Delete(Guid id)
        {
            aspnet_Roles aspnet_roles = db.aspnet_Roles.Find(id);
            return View(aspnet_roles);
        }
        #endregion
        #region Eliminar Roles HttpPost
        //
        // POST: /Aspnet_Roles/Delete/5
        /// <summary>
        /// Elimina El rol que corresponda al identificador
        /// </summary>
        /// <param name="id">Identificador del Rol a Eliminar</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            aspnet_Roles aspnet_roles = db.aspnet_Roles.Find(id);
            db.aspnet_Roles.Remove(aspnet_roles);
            db.SaveChanges();
            TempData["Success"] = "Se ha Eliminado el rol " + aspnet_roles.RoleName + " correctamente";
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