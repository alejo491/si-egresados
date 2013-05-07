using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionBase.Controllers
{
    public class WizardController : Controller
    {

        public ActionResult Index(int actualStep = 0)
        {
            @ViewBag.ActualStep = actualStep;

            Session["Wizard"] = "1";

            switch (actualStep)
            {
                case 0:
                    @ViewBag.ActualRoute = @"/Topic/Index?wizardStep=0";
                    break;
                case 1:
                    @ViewBag.BackRoute = @ViewBag.ActualRoute;
                    @ViewBag.ActualRoute = @"/Experience/Index?wizardStep=0";
                    break;
                case 2:
                    @ViewBag.ActualRoute = @"/Study/Index?wizardStep=0";
                    break;
                case 3:
                    @ViewBag.ActualRoute = @"/User/Index?wizardStep=0";
                    break;

            }
            return View();
        }
        //
        // GET: /Wizard/
        /*
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Wizard/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Wizard/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Wizard/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Wizard/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Wizard/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Wizard/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Wizard/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    
         */
    }
}
