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

        public JsonResult ListActions(string field, string campo)
        {
            Random r = new Random();
            Dictionary<string, string> d; 
            if (Session["Dictionary"] == null)
            {
                d = new Dictionary<string, string>();
                Session["Dictionary"] = d;
            }
            else
            {
                d = (Dictionary<string, string>)Session["Dictionary"];
            }

            List<String> list = new List<string>();
            if (field != "Seleccione una opcion")
            {
                int n = r.Next(109);
                if (n%2 == 0)
                {

                    list.Add("Suma");
                    list.Add("Minimo");
                    list.Add("Maximo");
                    list.Add("Contar");
                    list.Add("Promedio");
                }
                else
                {
                    list.Add("SI");
                    list.Add("NO");
                }
                if (d.ContainsKey(campo))
                {
                    d[campo] = field;
                    Session["Dictionary"] = d;
                }
                else
                {
                    d.Add(campo, field);
                    Session["Dictionary"] = d;
                }
            }
            else
            {
                if (d.ContainsKey(campo))
                {
                    d.Remove(campo);
                    Session["Dictionary"] = d;
                }
            }
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
            
            for (int i = 0; i < 10000; i++ )
            {
                Dictionary<string, string> d = (Dictionary<string, string>)Session["Dictionary"];               
                if (d != null)
                {
                    foreach (var val in d)
                    {
                        if (!list.Contains(val.Value))
                        {
                            list.Add(val.Value);
                        }

                    }
                }
            }
            
            
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
