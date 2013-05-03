using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
   

    public class SendSurveysController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        
        private bool egresados;
        private bool jefeEgresados;
        private bool ingenieriadeSistemas;
        private bool ingenieriaElectronica;
        private bool ingenieriaAutomatica;
        private bool telematica;
        private string fechaDesde;
        private string fechaHasta;
        private string asunto;
        private string mensaje;
        private Guid idObtenido;
        private bool continuar;
        private bool continuar2 = false;
        CultureInfo provider = CultureInfo.InvariantCulture;
        public Regex re;

        //
        // GET: /SendSurveys/

        public ActionResult Send(Guid id)
        {
            ViewBag.Seleccionado1 = true;
            ViewBag.Seleccionado2 = false;
            ViewBag.Seleccionado3 = false;
            ViewBag.Seleccionado4 = false;
            ViewBag.Seleccionado5 = false;
            ViewBag.Seleccionado6 = false;
            ViewBag.FechaDesde = "";
            ViewBag.FechaHasta = "";
            re = new Regex("^(0?[1-9]|1[0-9]|2|2[0-9]|3[0-1])/(0?[1-9]|1[0-2])/(d{2}|d{4})$");

            return View();
           // return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public ActionResult Send(Guid id, FormCollection form)
        {
            var selected= new Dictionary<string,string>();
            idObtenido = id;
            continuar = false;
            continuar2 = false;
            TempData["Error3"] = "¡Es Obligatorio Colocar un Asunto!";
            TempData["Error4"] = "¡Es Obligatorio Colocar un Mensaje!";

            #region Validaciones de Seleccion de Destinatario

            foreach (string v in form)
            {
                var k = form[v];
                if (continuar)
                {
                    TempData["Error1"] = "";
                    break;
                }

                switch (v)
                {
                    case "Dirigida":

                        if (k == "True")
                        {
                            ViewBag.Seleccionado1 = true;
                            /*
                            ViewBag.Seleccionado2 = false;
                          * */
                            continuar = true;
                            egresados = true;
                            jefeEgresados = false;
                          
                        }
                        else
                        {
                           
                            ViewBag.Seleccionado1 = false;
                            /* ViewBag.Seleccionado2 = true;
                             */
                            continuar = true;
                            egresados = false;
                            jefeEgresados = true;
                            
                        }
                        break;
                }
            }
            #endregion

            #region Validaciones de un Programa

            foreach (string variable in form)
            {
                var k = form[variable];
                switch (variable)
                {

                    case "IngSistemas":
                        ingenieriadeSistemas = k.Contains("true");
                        if (k.Contains("true"))
                        {
                            ViewBag.Seleccionado3 = true;
                        }
                        else
                        {
                            ViewBag.Seleccionado3 = false;
                        }
                        break;
                    case "IngElectronica":
                        ingenieriaElectronica = k.Contains("true");
                        if (k.Contains("true"))
                        {
                            ViewBag.Seleccionado4 = true;
                        }
                        else
                        {
                            ViewBag.Seleccionado4 = false;
                        }
                        break;
                    case "IngAutomatica":
                        ingenieriaAutomatica = k.Contains("true");
                        if (k.Contains("true"))
                        {
                            ViewBag.Seleccionado5 = true;
                        }
                        else
                        {
                            ViewBag.Seleccionado5 = false;
                        }
                        break;
                    case "Telematica":
                        telematica = k.Contains("true");
                        if (k.Contains("true"))
                        {
                            ViewBag.Seleccionado6 = true;
                        }
                        else
                        {
                            ViewBag.Seleccionado6 = false;
                        }
                        break;
                }
            }
            if (!(ingenieriadeSistemas) && !(ingenieriaAutomatica) && !(ingenieriaElectronica) && !(telematica))
            {
                TempData["Error2"] = "¡Es Obligatorio Seleccionar como Minimo un Programa!";
            }
            
            #endregion

            #region Se Valida la Fecha y se permite continuar si es Valida

            /*
             string []format = new string []{"yyyy-MM-dd HH:mm:ss"};
             string value = "2011-09-02 15:30:20";
             DateTime datetime;
             if (DateTime.TryParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.NoCurrentDateDefault  , out datetime))
             Console.WriteLine("Valido  : " + datetime);
             else
             Console.WriteLine("Invalido");
             */
            foreach (string variable in form)
            {
                var k = form[variable];
                switch (variable)
                {

                    case "txtFechaDesde":
                        ViewBag.FechaDesde = k;
                        fechaDesde = k;
                        break;
                    case "txtFechaHasta":
                        ViewBag.FechaHasta = k;
                        fechaHasta = k;
                        break;
                }
            }

            string[] format = new string[] { "yyyy-MM-dd" };
            string[] formatE = new string[] { "MM-dd-yyyy" };
            string[] formatV = new string[] { "dd-MM-yyyy" };

            DateTime fecha;
            if (fechaDesde.Contains("/"))
            {
                fechaDesde=fechaDesde.Replace('/', '-');
            }
            if (fechaHasta.Contains("/"))
            {
               fechaHasta= fechaHasta.Replace('/','-');
            }

            if (fechaDesde !="")
            {
                if (DateTime.TryParseExact(fechaDesde,format,System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.NoCurrentDateDefault,out fecha))
                {
                    if (Convert.ToDateTime(fechaDesde) > DateTime.Now)
                    {
                        TempData["Error1"] = "¡La Fecha Egresados Desde No pueden ser mayores al dia actual!";
                        continuar = false;
                 
                    }
                }
                else if (DateTime.TryParseExact(fechaDesde, formatE, System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.NoCurrentDateDefault, out fecha))
                {
                    string fechaConFormato = string.Empty;

                    //formatea la fecha si viene en formato mm-dd-yyyy
                    fechaConFormato = Regex.Replace(fechaDesde,
                    @"(?<mm>\d{1,2})-(?<dd>\d{1,2})\b-\b(?<yyyy>\d{4})",
                    "${yyyy}-${mm}-${dd}");
                    fechaDesde = fechaConFormato;

                    if (Convert.ToDateTime(fechaDesde) > DateTime.Now)
                    {
                        TempData["Error1"] = "¡La Fecha Egresados Desde No pueden ser mayores al dia actual!";
                        continuar = false;

                    }

                }
                else if (DateTime.TryParseExact(fechaDesde, formatV, System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.NoCurrentDateDefault, out fecha))
                {
                    string fechaConFormato = string.Empty;

                    //formatea la fecha si viene en formato dd-mm-yyyy
                    fechaConFormato = Regex.Replace(fechaDesde,
                    @"(?<dd>\d{1,2})\b-(?<mm>\d{1,2})-\b(?<yyyy>\d{4})",
                    "${yyyy}-${mm}-${dd}");
                    fechaDesde = fechaConFormato;

                    if (Convert.ToDateTime(fechaDesde) > DateTime.Now)
                    {
                        TempData["Error1"] = "¡La Fecha Egresados Desde No pueden ser mayores al dia actual!";
                        continuar = false;

                    }

                }
                else
               {
                    TempData["Error1"] = "¡La Fecha No tiene un Formato Valido!";
                    continuar = false;
                }
            }
            if (fechaHasta != "")
            {
                if (DateTime.TryParseExact(fechaHasta,format,System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.NoCurrentDateDefault,out fecha))
                {
                    if (Convert.ToDateTime(fechaHasta) > DateTime.Now)
                    {
                        TempData["Error1"] = "¡La Fecha Egresados Hasta No pueden ser mayores al dia actual!";
                        continuar = false;
                    }
                    else
                    {
                        TempData["Error1"] = "";
                        continuar = true;
                    }
                }
                else if (DateTime.TryParseExact(fechaHasta, formatE, System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.NoCurrentDateDefault, out fecha))
                {
                    string fechaConFormato = string.Empty;

                    //formatea la fecha si viene en formato mm-dd-yyyy
                    fechaConFormato = Regex.Replace(fechaHasta,
                    @"(?<mm>\d{1,2})-(?<dd>\d{1,2})\b-\b(?<yyyy>\d{4})",
                    "${yyyy}-${mm}-${dd}");
                    fechaHasta = fechaConFormato;

                    if (Convert.ToDateTime(fechaHasta) > DateTime.Now)
                    {
                        TempData["Error1"] = "¡La Fecha Egresados Hasta No pueden ser mayores al dia actual!";
                        continuar = false;
                    }
                    else
                    {
                        TempData["Error1"] = "";
                        continuar = true;
                    }

                }
                else if (DateTime.TryParseExact(fechaHasta, formatV, System.Globalization.CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.NoCurrentDateDefault, out fecha))
                {
                    string fechaConFormato = string.Empty;

                    //formatea la fecha si viene en formato dd-mm-yyyy
                    fechaConFormato = Regex.Replace(fechaHasta,
                    @"(?<dd>\d{1,2})\b-(?<mm>\d{1,2})-\b(?<yyyy>\d{4})",
                    "${yyyy}-${mm}-${dd}");
                    fechaHasta = fechaConFormato;
                    
                    if (Convert.ToDateTime(fechaHasta) > DateTime.Now)
                    {
                        TempData["Error1"] = "¡La Fecha Egresados Hasta No pueden ser mayores al dia actual!";
                        continuar = false;
                    }
                    else
                    {
                        TempData["Error1"] = "";
                        continuar = true;
                    }

                }
                else
                {
                    TempData["Error1"] = "¡La Fecha No tiene un Formato Valido!";
                    continuar = false;
                }
            }
            if (continuar)
            {
                if (fechaDesde != "" && fechaDesde != "")
                {
                    if (Convert.ToDateTime(fechaDesde) > Convert.ToDateTime(fechaHasta))
                    {
                        TempData["Error1"] = "¡La Fecha Desde no puede ser mayor a Fecha Hasta !";
                        continuar = false;
                    }
                    else
                    {
                        continuar = true;
                    }
                }
            }
            #endregion

            #region Validar si Asunto esta lleno

            foreach (string v in form)
            {
                var k = form[v];
                if (continuar2)
                {
                    TempData["Error3"] = "";
                    break;
                }

                switch (v)
                {   
                    case "txtAsunto":
                        if (k == "")
                        {
                            TempData["Error3"] = "¡Es Obligatorio Colocar un Asunto!";
                            continuar2 = false;
                        }
                        else
                        {
                            continuar2 = true;
                            ViewBag.Asunto = k;
                            asunto = k;
                        }
                        break;
                }
            }
            #endregion

            #region Validar si Mensaje esta lleno

            continuar2 = false;
            foreach (string v in form)
            {
                var k = form[v];
                if (continuar2)
                {
                    TempData["Error4"] = "";
                    break;
                }

                switch (v)
                {
                        case "txtMensaje":
                        if (k == "")
                        {
                            TempData["Error4"] = "¡Es Obligatorio Colocar un Mensaje!";
                            continuar2 = false;
                        }
                        else
                        {
                            if (asunto !=null)
                            {
                                continuar2 = true;
                            }
                            ViewBag.Mensaje = k;
                            TempData["Error4"] = "";
                            mensaje = k;
                        }
                        break;
                }
            }
            #endregion

            if (continuar==true && continuar2==true)
            {
                /*
                foreach (string variable in form)
                {
                    var k = form[variable];
                    switch (variable)
                    {
                        case "Egresados":
                            egresados = k.Contains("true");
                            break;
                        case "JefesEgresados":
                            jefeEgresados = k.Contains("true");
                            break;
                        case "IngSistemas":
                            ingenieriadeSistemas = k.Contains("true");
                            break;
                        case "IngElectronica":
                            ingenieriaElectronica = k.Contains("true");
                            break;
                        case "IngAutomatica":
                            ingenieriaAutomatica = k.Contains("true");
                            break;
                        case "Telematica":
                            telematica = k.Contains("true");
                            break;
                        case "txtFechaDesde":
                            fechaDesde = k;
                            break;
                        case "txtFechaHasta":
                            fechaHasta = k;
                            break;
                        case "txtAsunto":
                            asunto = k;
                            break;
                        case "txtMensaje":
                            mensaje = k;
                            break;
                    }
                }
                */
                if (jefeEgresados)
                {
                    #region Jefes de Egresados en Fechas Especificas y por Programas Seleccionados
                    if (fechaDesde != "" && fechaHasta != "")
                    {
                        DateTime fd = (Convert.ToDateTime(fechaDesde));
                        DateTime fh = (Convert.ToDateTime(fechaHasta));

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas",fd,fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica", fd, fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica", fd, fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica", fd, fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                    }
                    #endregion

                    #region Jefes de Egresados desde una Fecha, hasta la Actual, por Programas Seleccionados
                    else if (fechaHasta == "" && fechaDesde!="")
                    {
                        DateTime fd =(Convert.ToDateTime(fechaDesde));

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas",fd, DateTime.Now);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica", fd, DateTime.Now);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica", fd, DateTime.Now);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica", fd, DateTime.Now);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                    }
                    #endregion

                    #region Jefes de Egresados Hasta una fecha, por Programas Seleccionados
                    else if (fechaDesde=="" && fechaHasta!="")
                    {
                        DateTime fh = Convert.ToDateTime(fechaHasta);

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas",
                                                                                fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica",
                                                                                fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica",
                                                                                fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica",
                                                                                fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                    }
                    #endregion

                    #region Todos los Jefes de Egresados, por Programas Seleccionados
                    else if (fechaDesde == "" && fechaHasta != "")
                    {
                       if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas");
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica");
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica");
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica");
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                    }
                    #endregion
                }

                if (egresados)
                {
                    #region Egresados entre Fechas y Por Programas Seleccionados
                    if (fechaDesde != "" && fechaHasta != "")
                    {
                        DateTime fd = (Convert.ToDateTime(fechaDesde));
                        DateTime fh = (Convert.ToDateTime(fechaHasta));

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas", fd, fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica",  fd, fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica", fd, fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica", fd, fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                    }
                    #endregion

                    #region Egresados desde una Fecha Especifica hasta la actualidad por Programas Seleccionados
                    else if (fechaHasta == ""&& fechaDesde!="")
                    {
                        DateTime fd = (Convert.ToDateTime(fechaDesde));

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas", fd, DateTime.Now);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica", fd, DateTime.Now);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica", fd, DateTime.Now);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica", fd, DateTime.Now);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                    }
                    #endregion

                    #region Todos los Egresados, por Programa Seleccionado
                    else if (fechaDesde=="" && fechaHasta!="")
                    {
                        DateTime fh = (Convert.ToDateTime(fechaHasta));

                        if (ingenieriadeSistemas)
                        {
                            Dictionary<string, string> c =
                                SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas",
                                                                                fh);
                           selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);

                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica",
                                                                                    fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica",
                                                                                    fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica",
                                                                                    fh);
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                    }
                    #endregion

                    #region Egresados entre Fechas y Por Programas Seleccionados

                    if (fechaDesde == "" && fechaHasta == "")
                    {
                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas");
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica");
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica");
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica");
                            selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                        }
                    }
                    #endregion
                }
                /*
                if (egresados != true && jefeEgresados != true)
                {
                    if (ingenieriadeSistemas)
                    {
                        var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas",
                                                                                Convert.ToDateTime(fechaDesde));
                        selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                    }
                    else if (ingenieriaAutomatica)
                    {
                        var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica",
                                                                                Convert.ToDateTime(fechaDesde));
                        selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                    }
                    else if (ingenieriaElectronica)
                    {
                        var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica",
                                                                                Convert.ToDateTime(fechaDesde));
                        selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                    }
                    else if (telematica)
                    {
                        var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica",
                                                                                Convert.ToDateTime(fechaDesde));
                        selected = selected.Concat(c).ToDictionary(x => x.Key, x => x.Value);
                    }
                }
                */
                //selected.Add("jaimealberto.jurado@gmail.com","Jaime Jurado");

                //Agregar validacion de que si no encuentra ningun usuario no pueda continuar
                //La Concatenacion no esta funcionando

                if (selected.Count == 0)
                {
                    TempData["ErrorDatos"] = "No se puede continuar, No Se encontraron Datos";
                    return View();
                }

                TempData["message"] = ViewBag.Mensaje;
                TempData["subject"] = ViewBag.Asunto;
                TempData["d"] = selected;

                return RedirectToAction("Preview", new {id});
            }
            return View();
        }

        public ActionResult Preview(Guid id) {

            //var name = ((Dictionary<string, string>)TempData["d"])["jaimejn@unicauca.edu.co"];
            var survey = (Survey)db.Surveys.Find(id);
            ViewBag.Survey = survey;
            var message = (string)TempData["message"];
            var subject = (string)TempData["subject"];
            var url = Url.Action("Fill", "FillSurvey", new { id });
            ViewData["body"] = SendSurveysEmailController.PopulateBody("UserName", survey.Name, url, message);
            TempData["message"] = message;
            TempData["title"] = survey.Name;
            TempData["id"] = survey.Id;
            ViewBag.Subject = subject;
            TempData["subject"] = subject;
            return View();
        }

        [HttpPost]
        public ActionResult Preview(FormCollection form) {

            var recipients = (Dictionary<string, string>)TempData["d"];
            var message = (string)TempData["message"];
            var subject = (string)TempData["subject"];
            var title = (string)TempData["title"];



            foreach (string item in recipients.Keys) {
                var url = Url.Action("Fill", "FillSurvey", new { id = (Guid)TempData["id"] , item });
                var body = SendSurveysEmailController.PopulateBody(recipients[item], title, url, message);
                SendSurveysEmailController.SendHtmlFormattedEmail(item, subject, body);
            }

            return RedirectToAction("Index", "Surveys");

        }

        //
        // GET: /SendSurveys/

        public ActionResult SendSpecific(Guid id)
        {
            return View();
        }

        //
        // POST: /SendSurveys/SendSpecific
        [HttpPost]
        public ActionResult SendSpecific(Guid id, FormCollection form)
        {
            var selected = new Dictionary<string, string>();
            string nombreCompleto = "";
            TempData["ErrorD"] = "¡Es Obligatorio Seleccionar un Destinatario!";
            TempData["ErrorA"] = "¡Es Obligatorio Colocar un Asunto!";
            TempData["ErrorM"] = "¡Es Obligatorio Colocar un Mensaje!";

            #region Validar si Destinatario esta lleno

            continuar = false;

            foreach (string variable in form)
            {
                var k = form[variable];
                if (continuar)
                {
                    TempData["ErrorD"] = "";
                    break;
                }

                switch (variable)
                {
                    case "txtDestinatario":
                        if (k == "")
                        {
                            TempData["ErrorD"] = "¡Es Obligatorio Seleccionar un Destinatario!";
                            continuar = false;
                        }
                        else
                        {
                            TempData["ErrorD"] = "";
                            nombreCompleto = k;
                        }
                        break;
                }
            }
            #endregion

            #region Validar si Asunto esta lleno

            foreach (string variable in form)
            {
                var k = form[variable];

                if (continuar)
                {
                    TempData["ErrorA"] = "";
                    break;
                }

                switch (variable)
                {
                    case "txtAsunto":
                        if (k == "")
                        {
                            TempData["ErrorA"] = "¡Es Obligatorio Colocar un Asunto!";
                            continuar = false;
                        }
                        else
                        {
                            if (nombreCompleto != "")
                            {
                                continuar = true;
                            }
                            ViewBag.Asunto = k;
                            asunto = k;
                        }
                        break;
                }
            }
            #endregion

            #region Validar si Mensaje esta lleno

            continuar = false;

            foreach (string variable in form)
            {
                var k = form[variable];
                if (continuar)
                {
                    TempData["ErrorM"] = "";
                    break;
                }

                switch (variable)
                {
                    case "txtMensaje":
                        if (k == "")
                        {
                            TempData["ErrorM"] = "¡Es Obligatorio Colocar un Mensaje!";
                            continuar = false;
                        }
                        else
                        {
                            if (asunto != null)
                            {
                                continuar = true;
                            }
                            ViewBag.Mensaje = k;
                            TempData["ErrorM"] = "";
                            mensaje = k;
                        }
                        break;
                }
            }
            #endregion

            if (continuar)
            {
                var usuario = db.Users.Where(u => u.FirstNames + " " + u.LastNames == nombreCompleto);
                foreach (var user in usuario)
                {
                    var idUsuario = user.Id;
                    var memberShip = db.aspnet_Membership.Find(idUsuario);
                    selected.Add(memberShip.Email, nombreCompleto);
                }

                TempData["subject"] = asunto;
                TempData["message"] = mensaje;
                TempData["d"] = selected;

                return RedirectToAction("Preview", new { id });
            }

            return View();
            
        }

        public ActionResult Autocomplete(string term)
        {
            var items = (from u in db.Users select u.FirstNames + " " + u.LastNames).ToArray();
            var filteredItems = items.Where(
                item => item.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
    }
}
