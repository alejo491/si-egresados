using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
namespace AplicacionBase.Controllers
{
    public class VerifyController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        //
        // GET: /Verify/

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
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Create", "User");
        }

    }
}
