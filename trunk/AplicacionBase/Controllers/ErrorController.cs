using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionBase.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ViewResult Index(string aspxerrorpath)
        {
            
            if (aspxerrorpath != null)
            {
                ViewBag.ErrorType = "La pagina "+aspxerrorpath+" no existe";
            }
            return View();
        }

        public ViewResult ErrorDataUpdate()
        {
            return View();
        }

    }
}
