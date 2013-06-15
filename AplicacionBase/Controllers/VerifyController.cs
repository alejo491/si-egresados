using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
namespace AplicacionBase.Controllers
{
    /// <summary>
    /// Controlador que apoya al UserController verificando direccionamientos
    /// </summary>
    public class VerifyController : Controller
    {
        /// <summary>
        /// Atributo que consulta la base de datos.
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Verify/
        /// <summary>
        /// Metodo que ayuda al direccionamiento a la hora de autenticarse en el sistema
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {
                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }
            }
            foreach (var e2 in db.Users)
            {
                if (e2.Id == g)
                {
                    var steps = db.UsersSteps.Where(s => s.UserId == e2.Id).OrderBy(s => s.Step.SOrder);

                    if (!steps.Any())
                    {
                        return RedirectToAction("Begin", "User", new { id = g });
                    }
                    else
                    {
                        var tmp = (List<UsersStep>)steps.ToList();
                        Session["steps"] = tmp;
                        var ActualStep = Convert.ToInt16(tmp.ElementAt(0).Step.SOrder);
                        return RedirectToAction("Index", "Wizard", ActualStep);
                    }
                }
            }
            return RedirectToAction("Create", "User");
        }

        /// <summary>
        /// Metodo que ayuda al direccionamiento dependiendo del usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {
                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }
            }
            foreach (var e2 in db.Users)
            {
                if (e2.Id == g)
                {
                    return RedirectToAction("Edit", "User", new { id = g });
                }
            }
            return RedirectToAction("Create", "User");
        }

        /// <summary>
        /// Metodo que verifica si el usuario tiene el rol de administrador
        /// </summary>
        /// <param name="id">Id del usuario que ingresa al sistema</param>
        /// <returns>TRUE, si tiene el rol administrador, sino FALSE</returns>
        public Boolean Begin(Guid id)
        {
            User user = db.Users.Find(id);
            bool roluser = false;

            foreach (var ca in db.aspnet_Roles)
            {
                if (ca.LoweredRoleName == "administrador")
                {
                    aspnet_UsersInRoles v = db.aspnet_UsersInRoles.Find(user.Id, ca.RoleId);
                    if (v != null)
                    {
                        roluser = true;
                    }
                    else { roluser = false; }
                }
            }
            return roluser;
        }
    }
}
