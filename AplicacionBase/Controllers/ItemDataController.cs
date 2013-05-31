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
            ItemData item = new ItemData();
            item.Id = Guid.NewGuid();
            item.IdReport = id;
            item.SQLQuey = "";
            var ListCampos = new List<Field>();
            var ListFiltros = new List<AplicacionBase.Models.Filter>();
            var ListGrupos = new List<GroupOption>();
            var Lcamposgroup = new List<Field>(); 
            int bandsql = 0;
            int bandcamp = 0;
            int bandfilt = 0;
            string auxlogic = "";
            string auxsqlcampos = "";
            string auxsqlfiltros = "";
            string auxsqlgrupos = "";
            string SQL = "";
            foreach (String key in form)
            {
                var k = form[key];
                if (key == "AllFields" && k == "true,false")
                {
                    SQL = "select * from ConsultaGeneral";
                    bandsql++;
                }

                if (key == "CountFields" && k == "true,false")
                {
                    SQL = "select count(*) from ConsultaGeneral";
                    bandsql++;
                }

                if (key.StartsWith("field"))
                {
                    if (key.StartsWith("field") && k != "Seleccione una opcion" && k != "Año" && k != "Mes" && k != "Dia" && k != "Suma" && k != "Minimo" && k != "Maximo" && k != "Contar" && k != "Promedio")
                    {
                        Field objcampo = new Field();
                        objcampo.Id = Guid.NewGuid();
                        objcampo.IdItemData = item.Id;
                        bandcamp = 1;
                        objcampo.FieldName = k;
                        ListCampos.Add(objcampo);
                        bandsql++;
                    }

                    if (key.StartsWith("fieldaction") && bandcamp == 1)
                    {
                        if( k == "Seleccione una opcion")
                        {
                            ListCampos.Last().FieldOperation = "";
                        }
                        if (k == "Suma")
                        {
                            ListCampos.Last().FieldOperation = "SUM";
                        }
                        if (k == "Minimo")
                        {
                            ListCampos.Last().FieldOperation = "MIN";
                        }
                        if (k == "Maximo")
                        {
                            ListCampos.Last().FieldOperation = "MAX";
                        }
                        if (k == "Contar")
                        {
                            ListCampos.Last().FieldOperation = "COUNT";
                        }
                        if (k == "Promedio")
                        {
                            ListCampos.Last().FieldOperation = "AVG";
                        }
                        if (k == "Año")
                        {
                            ListCampos.Last().FieldOperation = "YEAR";
                        }
                        if (k == "Mes")
                        {
                            ListCampos.Last().FieldOperation = "MONTH";
                        }
                        if (k == "Dia")
                        {
                            ListCampos.Last().FieldOperation = "DAY";
                        }
                        bandcamp = 0;
                       // db.Fields.Add(ListCampos.Last());
                        //db.SaveChanges();
                    }

                }

                if (key.StartsWith("searchfield") && k != "Seleccione una opcion")
                {
                    if (ListFiltros.Count == 0)
                    {
                        AplicacionBase.Models.Filter objfiltro = new AplicacionBase.Models.Filter();
                        objfiltro.Id = Guid.NewGuid();
                        objfiltro.IdItemData = item.Id;
                        bandfilt = 1;
                        objfiltro.FieldName = k;
                        objfiltro.LogicOperator = "WHERE";
                        ListFiltros.Add(objfiltro);
                        if (auxlogic != "")
                        {
                            auxlogic = "";
                        }
                    }
                    if(auxlogic != "Condicion" && auxlogic != "")
                    {
                        AplicacionBase.Models.Filter objfiltro = new AplicacionBase.Models.Filter();
                        objfiltro.Id = Guid.NewGuid();
                        objfiltro.IdItemData = item.Id;
                        bandfilt = 1;
                        if (ListFiltros.Count == 0)
                        {
                            objfiltro.LogicOperator = "WHERE";
                        }
                        else
                        {
                            objfiltro.LogicOperator = auxlogic;
                        }
                        objfiltro.FieldName = k;
                        ListFiltros.Add(objfiltro);
                        auxlogic = "";
                    }
                  
                }

                if (key.StartsWith("operator") && bandfilt == 1)
                {
                    if(k == "Operacion")
                    {
                        bandfilt = 0;
                        ListFiltros.RemoveAt(ListFiltros.Count-1);
                    }
                    if (k == "Mayor_o_igual_a")
                    {
                        ListFiltros.Last().Operator = ">=";
                    }
                    if (k == "Menor_o_igual_a")
                    {
                        ListFiltros.Last().Operator = "<=";
                    }
                    if (k == "Mayor_que")
                    {
                        ListFiltros.Last().Operator = ">";
                    }
                    if (k == "Menor_que")
                    {
                        ListFiltros.Last().Operator = "<";
                    }
                    if (k == "Igual_a")
                    {
                        ListFiltros.Last().Operator = "=";
                    }
                    if (k == "Diferente_de")
                    {
                        ListFiltros.Last().Operator = "<>";
                    }
                    if (k == "Like")
                    {
                        ListFiltros.Last().Operator = "LIKE";
                    }
                }

                if (key.StartsWith("criteria") && bandfilt == 1)
                {
                    if(k == "")
                    {
                        ListFiltros.RemoveAt(ListFiltros.Count - 1);
                        bandfilt = 0;
                    }
                    else
                    {
                        ListFiltros.Last().Value = k;
                        bandfilt = 0;
                        bandsql++;
                       // db.Filters.Add(ListFiltros.Last());
                        //db.SaveChanges();
                    }
                    
                }
                if (key.StartsWith("logic"))
                {
                    auxlogic = k;
                }
                if (key.StartsWith("group") && k != "Seleccione una opcion")
                {
                    GroupOption objgrupo = new GroupOption();
                    objgrupo.Id = Guid.NewGuid();
                    objgrupo.IdItemData = item.Id;
                    objgrupo.FieldName = k;
                    ListGrupos.Add(objgrupo);
                    bandsql++;
                    //db.GroupOptions.Add(objgrupo);
                    //db.SaveChanges();
                }

            }

            if (ListGrupos.Count == 0)
            {
                foreach (Field campo in ListCampos)
                {
                    if (campo.Id == ListCampos.First().Id)
                    {
                        auxsqlcampos = "SELECT ";
                    }
                    if (campo.FieldOperation != "")
                    {
                        auxsqlcampos = auxsqlcampos + campo.FieldOperation + "(" + campo.FieldName + ")";
                    }
                    else
                    {
                        auxsqlcampos = auxsqlcampos + campo.FieldName;
                    }

                    if (campo.Id != ListCampos.Last().Id)
                    {
                        auxsqlcampos = auxsqlcampos + ", ";
                    }
                    else
                    {
                        auxsqlcampos = auxsqlcampos + " FROM ConsultaGeneral";
                    }
                    
                }
            }

            foreach (AplicacionBase.Models.Filter filtro in ListFiltros)
            {
                auxsqlfiltros = auxsqlfiltros + filtro.LogicOperator + " " + filtro.FieldName + " " + filtro.Operator + " ";
                    if(filtro.Operator == "LIKE")
                    {
                        auxsqlfiltros = auxsqlfiltros + "'" + filtro.Value + "'" + " ";
                    }
                    else
                    {
                        auxsqlfiltros = auxsqlfiltros + filtro.Value + " ";
                    }
            }

            foreach (GroupOption grupo in ListGrupos)
            {
                if (auxsqlgrupos == "")
                {
                    auxsqlgrupos = "GROUP BY ";
                }
                auxsqlgrupos = auxsqlgrupos + grupo.FieldName;
                if (grupo.Id != ListGrupos.Last().Id)
                {
                    auxsqlgrupos = auxsqlgrupos + "," + " ";
                }
            }
        //    ListGrupos = ListGrupos.Distinct().ToList();
        //    ListCampos = ListCampos.Distinct().ToList();

            if (SQL == "" && bandsql == 0)
            {
                auxsqlcampos = "SELECT * ConsultaGeneral";
            }
            else
            {
                if (ListGrupos.Count > 0 && ListCampos.Count > 0)
                {
                    foreach (Field nuevofield in ListCampos)
                    {
                        foreach (GroupOption nuevogroup in ListGrupos)
                        {

                            if (nuevofield.FieldName == nuevogroup.FieldName )
                            {
                                Lcamposgroup.Add(nuevofield);
                            }
                            if (nuevofield.FieldName != nuevogroup.FieldName && nuevofield.FieldOperation != "")
                            {
                                Lcamposgroup.Add(nuevofield);
                            }
                        }
                    }
                    Lcamposgroup = Lcamposgroup.Distinct().ToList();
                    foreach (Field campo in Lcamposgroup)
                    {
                        if (campo.Id == Lcamposgroup.First().Id)
                        {
                            auxsqlcampos = "SELECT ";
                        }
                        if (campo.FieldOperation != "")
                        {
                            auxsqlcampos = auxsqlcampos + campo.FieldOperation + "(" + campo.FieldName + ")";
                        }
                        else
                        {
                            auxsqlcampos = auxsqlcampos + campo.FieldName;
                        }

                        if (campo.Id != Lcamposgroup.Last().Id)
                        {
                            auxsqlcampos = auxsqlcampos + ", ";
                        }
                        else
                        {
                            auxsqlcampos = auxsqlcampos + " FROM ConsultaGeneral ";
                        }
                    }
                    if (Lcamposgroup.Count == 0)
                    {
                        auxsqlcampos = "SELECT * fROM ConsultaGeneral";
                        auxsqlgrupos = "";
                    }
                }                    
            }           
            SQL = auxsqlcampos + auxsqlfiltros + auxsqlgrupos;
            item.SQLQuey = SQL;
         //  db.ItemDatas.Add(item);
         //   db.SaveChanges();

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
                list.Add(@"Mayor_o_igual_a");
                list.Add(@"Menor_o_igual_a");
                list.Add(@"Mayor_que");
                list.Add(@"Menor_que");
                list.Add(@"Igual_a");
                list.Add(@"Diferente_de");
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
