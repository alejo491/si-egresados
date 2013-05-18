using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionBase.Controllers
{
    public class ItemDataController : Controller
    {
        //
        // GET: /ItemData/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListLogic()
        {
            List<String> list = new List<string>();
            list.Add("AND");
            list.Add("OR");
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListActions()
        {
            List<String> list = new List<string>();
            list.Add("Suma");
            list.Add("Minimo");
            list.Add("Maximo");
            list.Add("Contar");
            list.Add("Promedio");
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListOperators()
        {
            List<String> list = new List<string>();
            list.Add(@"Mayor o igual");
            list.Add(@"Menor o igual");
            list.Add(@"Mayor");
            list.Add(@"Menor");
            list.Add(@"Igual");
            list.Add(@"Diferente de");
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListSearchFields()
        {
            List<String> list = new List<string>();
            list.Add("SearchField1");
            list.Add("SearchField2");
            list.Add("SearchField3");
            list.Add("SearchField4");
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListGroupFields()
        {
            List<String> list = new List<string>();
            list.Add("GroupField1");
            list.Add("GroupField1");
            list.Add("GroupField1");
            list.Add("GroupField1");
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListFields()
        {
            List<String> list = new List<string>();
            list.Add("Campo1");
            list.Add("Campo2");
            list.Add("Campo3");
            list.Add("Campo4");
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            foreach (String key in form)
            {
                var k = form[key];
            }
            return RedirectToAction("Index", "Home");
        }

    }


}
