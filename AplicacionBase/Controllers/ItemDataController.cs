using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    public class ItemDataController : Controller
    {
        //
        // GET: /ItemData/
        
        public readonly DbSIEPISContext db = new DbSIEPISContext();

        public ActionResult Index(Guid id)
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult Index(Guid  id, FormCollection form)
        {
           /* bool allfields = false;
            bool somefield = false;
            foreach (String key in form)
            {
                if (key.Contains("AllFields"))
                {
                    if (form[key].Contains("true"))
                    {
                        allfields = true;
                    }
                }

                if (key.Substring(0,5) == "field")
                {
                    if (form[key] != "Seleccione una opcion")
                    {
                        somefield = true;
                    }
                }

            }
            */

            foreach (String key in form)
            {
                db.Fields.Add(new Field());
                db.SaveChanges();

            }

            return RedirectToAction("Index", "Home");
            
            

        }

        #region Ajax

        public JsonResult ListLogic()
        {
            List<String> list = new List<string>();
            list.Add("AND");
            list.Add("OR");
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }     

        public JsonResult ListActions(string field, string campo)
        {

            var assembly = new AssemblyHelper();
            var result = assembly.GetFieldsType();
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

                if (result[field] == "System.decimal" || result[field] == "Decimal" || result[field] == "decimal")
                {

                    list.Add("Suma");
                    list.Add("Minimo");
                    list.Add("Maximo");
                    list.Add("Contar");
                    list.Add("Promedio");
                }
                else
                {
                    var cadena = "System.Nullable`1[System.DateTime]".Substring(18, 15);
                    if (result[field] == @"System.DateTime")
                    {
                        list.Add("Año");
                        list.Add("Mes");
                        list.Add("Dia");
                        list.Add("Contar");
                    }
                    else
                    {
                        if (result[field].Length >= 15)
                        {
                            if (result[field].Substring(18, 15) == cadena)
                            {
                                list.Add("Año");
                                list.Add("Mes");
                                list.Add("Dia");
                                list.Add("Contar");
                            }
                            else
                            {
                                list.Add("Contar");
                            }
                        }
                        else
                        {
                            list.Add("Contar");
                        }
                    }

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

        public JsonResult ListOperators(string field, string campo)
        {
            List<String> list = new List<string>();
            var assembly = new AssemblyHelper();
            var result = assembly.GetFieldsType();
            if (result[field] == "System.string" || result[field] == "string" || result[field] == "System.String" || result[field] == "String")
            {
                list.Add("Like");
            }
            else
            {
                list.Add(@"Mayor o igual a");
                list.Add(@"Menor o igual a");
                list.Add(@"Mayor que");
                list.Add(@"Menor que");
                list.Add(@"Igual a");
                list.Add(@"Diferente de");
            }

            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListSearchFields()
        {
            var list = new List<string>();
            var assembly = new AssemblyHelper();
            var result = assembly.GetFieldsType();
            foreach (var key in result)
            {
                list.Add(key.Key);
            }
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListGroupFields()
        {
            var list = new List<string>();
            var assembly = new AssemblyHelper();
            var result = assembly.GetFieldsType();
            foreach (var key in result)
            {
                list.Add(key.Key);
            }

            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListFields()
        {
            var assembly = new AssemblyHelper();
            var result = assembly.GetFieldsType();
            var list = new List<string>();
            foreach (var opc in result)
            {
                list.Add(opc.Key);
            }

            /*list.Add("Campo1");
            list.Add("Campo2");
            list.Add("Campo3");
            list.Add("Campo4");*/
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

    }


}
