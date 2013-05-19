using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    public class WizardController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Wizard/

        public ActionResult Index(int ActualStep = 0)
        {
            @ViewBag.ActualStep = ActualStep;

            Session["Wizard"]="1";

            /*
            switch (ActualStep)
            {
                case 0:
                    @ViewBag.ActualRoute = @"/Topic/Index?wizardStep=1";
                    break;
                case 1:
                    @ViewBag.BackRoute = @ViewBag.ActualRoute;
                    @ViewBag.ActualRoute = @"/Topic/Index?wizardStep=1";
                    break;
                case 2:
                    @ViewBag.ActualRoute = @"/Surveys/Index?wizardStep=1";
                    break;
                case 3:
                    @ViewBag.ActualRoute = @"/FillSurveys/Fill?wizardStep=1";
                    break;

            }*/
            var steps = (List<UsersStep>)Session["steps"];
            if (steps != null) {
                var step = steps.ElementAt(ActualStep).Step;
                ViewBag.ActualRoute = step.SPath;
                ViewBag.StepsCount = steps.Count();
                ViewBag.StepId = step.Id;
                return View();
            }

            return RedirectToAction("Index", "Home");


        }

        public ActionResult Skip(int step, int end = 0)
        {
            //Add BD record
            /*var obj = GetActualUserStep((Guid)TempData["uStep"]);
            db.UsersSteps.Add(obj);
            db.SaveChanges();*/
            if (end != 0)
            {
                return RedirectToAction("End");
            }
            else
            {
                return RedirectToAction("Index", "Wizard", new { ActualStep = step });
            }
            
        }

        public ActionResult Next(int step, int end = 0)
        {
            //Remove BD record
            //var obj = db.UsersSteps.Find((Guid)Session["uStep"]);
            var sI = (Guid) TempData["uStep"];
            var uI = GetUserId();
            var obj = db.UsersSteps.Find(uI, sI);
            db.UsersSteps.Remove(obj);
            db.SaveChanges();
            if (end != 0)
            {
                return RedirectToAction("End");
            }
            else
            {
                return RedirectToAction("Index", "Wizard", new { ActualStep = step });
            }
            
        }

        public ActionResult End()
        {
            Session["Wizard"] = 0;
            return RedirectToAction("Index", "Home");
        }

        private UsersStep CreateUserStep(Guid sId)
        {
            var obj = new UsersStep {IdSteps = sId, Ok = "f"};
            foreach (var e in db.aspnet_Users.Where(e => e.UserName == HttpContext.User.Identity.Name))
            {
                obj.UserId = e.UserId;
            }

            return obj;
        }

        private Guid GetUserId()
        {
            var result = Guid.Empty;
            foreach (var e in db.aspnet_Users.Where(e => e.UserName == HttpContext.User.Identity.Name))
            {
                result = e.UserId;
            }
            return result;
        }
    }
}
