using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionBase.Controllers
{
    public class WizardController : Controller
    {
        //
        // GET: /Wizard/

        public ActionResult Index(int ActualStep = 0)
        {
            @ViewBag.ActualStep = ActualStep;
            switch (ActualStep)
            {
                case 0:
                    @ViewBag.ActualRoute = @"/Topic/Index?wizardStep=0";
                    break;
                case 1:
                    @ViewBag.BackRoute = @ViewBag.ActualRoute;
                    @ViewBag.ActualRoute = @"/Topic/Index?wizardStep=0";
                    break;
                case 2:
                    @ViewBag.ActualRoute = @"/Surveys/Index?wizardStep=0";
                    break;
                case 3:
                    @ViewBag.ActualRoute = @"/FillSurveys/Fill?wizardStep=0";
                    break;

            }
            return View();
        }
    }
}
