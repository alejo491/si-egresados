using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                        
                        if (k=="True")
                        {
                            ViewBag.Seleccionado1 = true;
                            ViewBag.Seleccionado2 = false;
                            continuar = true;
                            egresados = true;
                            jefeEgresados = false;
                        }
                        else
                        {
                            ViewBag.Seleccionado1 = false;
                            ViewBag.Seleccionado2 = true;
                            continuar = true;
                            egresados = false;
                            jefeEgresados = true;
                        }
                        break;
                    /*
                case "JefesEgresados":
                    continuar = k.Contains("true");
                    if (continuar)
                    {
                        ViewBag.Seleccionado2 = true;
                        ViewBag.Seleccionado1 = false;
                    }
                    else
                    {
                        ViewBag.Seleccionado2 = false;
                        ViewBag.Seleccionado1 = true;
                    }
                    break;
                    */
                }
            }

            /*
            if (ViewBag.Seleccionado1 == false )
            {
                TempData["Error1"] = "Debes seleccionar al menos un destinatario";
            }
            */
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
                    case "txtFechaDesde":
                        ViewBag.FechaDesde = k;
                        fechaDesde = k;
                        //DateTime d = Convert.ToDateTime(Convert.ToDateTime(fechaDesde).ToString("dd/MM/yyyy"));
                        break;
                    case "txtFechaHasta":
                        ViewBag.FechaHasta = k;
                        fechaHasta = k;
                        break;
                }
            }
            if (!(ingenieriadeSistemas) && !(ingenieriaAutomatica) && !(ingenieriaElectronica) && !(telematica))
            {
                TempData["Error2"] = "¡Es Obligatorio Seleccionar como Minimo un Programa!";
            }
            if (fechaDesde != "" && fechaHasta != "")
            {
                if (Convert.ToDateTime(fechaDesde)>Convert.ToDateTime(fechaHasta))
                {
                    TempData["Error1"] = "¡La Fecha Desde no puede ser mayor a Fecha Hasta !";
                    continuar = false;
                }

                if(Convert.ToDateTime(fechaDesde)>DateTime.Now || (Convert.ToDateTime(fechaHasta)>DateTime.Now))
                {
                    TempData["Error1"] = "¡La Fechas No pueden ser mayores al dia actual!";
                    continuar = false;
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

            #region Validar si Mensaje esta lleno y Almacenar Fechas

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
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica", fd, fh);
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica", fd, fh);
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica", fd, fh);
                            selected.Concat(c);
                        }
                    }
                    #endregion

                    #region Jefes de Egresados desde una Fecha, hasta la Actual, por Programas Seleccionados
                    else if (fechaHasta == "")
                    {
                        DateTime fd =(Convert.ToDateTime(fechaDesde));

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas",fd, DateTime.Now);
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica", fd, DateTime.Now);
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica", fd, DateTime.Now);
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica", fd, DateTime.Now);
                            selected.Concat(c);
                        }
                    }
                    #endregion

                    #region Jefes de Egresados Hasta una fecha, por Programas Seleccionados
                    else if (fechaDesde=="")
                    {
                        DateTime fh = Convert.ToDateTime(fechaHasta);

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas",
                                                                                fh);
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica",
                                                                                fh);
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica",
                                                                                fh);
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica",
                                                                                fh);
                            selected.Concat(c);
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
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica",  fd, fh);
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica", fd, fh);
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica", fd, fh);
                            selected.Concat(c);
                        }
                    }
                    #endregion

                    #region Egresados desde una Fecha Especifica hasta la actualidad por Programas Seleccionados
                    else if (fechaHasta == "")
                    {
                        DateTime fd = (Convert.ToDateTime(fechaDesde));

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas", fd, DateTime.Now);
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica", fd, DateTime.Now);
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica", fd, DateTime.Now);
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica", fd, DateTime.Now);
                            selected.Concat(c);
                        }
                    }
                    #endregion

                    #region Egresados inferiores o iguales a una fecha especifica, por Programa Seleccionado
                    else if (fechaDesde=="")
                    {
                        DateTime fh = (Convert.ToDateTime(fechaHasta));

                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas",
                                                                                    fh);
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica",
                                                                                    fh);
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica",
                                                                                    fh);
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica",
                                                                                    fh);
                            selected.Concat(c);
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
                        selected.Concat(c);
                    }
                    else if (ingenieriaAutomatica)
                    {
                        var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica",
                                                                                Convert.ToDateTime(fechaDesde));
                        selected.Concat(c);
                    }
                    else if (ingenieriaElectronica)
                    {
                        var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica",
                                                                                Convert.ToDateTime(fechaDesde));
                        selected.Concat(c);
                    }
                    else if (telematica)
                    {
                        var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica",
                                                                                Convert.ToDateTime(fechaDesde));
                        selected.Concat(c);
                    }
                }
                */
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
            var url = Url.Action("Fill", "FillSurvey");
            ViewData["body"] = SendSurveysEmailController.PopulateBody("UserName", survey.Name, url, message);
            TempData["message"] = message;
            TempData["title"] = survey.Name;
            return View();
        }

        [HttpPost]
        public ActionResult Preview(FormCollection form) {

            var recipients = (Dictionary<string, string>)TempData["d"];
            var message = (string)TempData["message"];
            var subject = (string)TempData["subject"];
            var title = (string)TempData["title"];



            foreach (string item in recipients.Keys) {
                var url = Url.Action("Fill", "FillSurvey");
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

            foreach (string variable in form)
            {
                var k = form[variable];

                switch (variable)
                {
                    case "FirstNames":
                        nombreCompleto = k;
                        break;
                    case "txtAsunto":
                        asunto = k;
                        break;
                    case "txtMensaje":
                        mensaje = k;
                        break;
                }
            }

            var listaUsuarios = db.Users.Where(u => u.FirstNames == nombreCompleto);
            foreach(var user in listaUsuarios) {
                var idUsuario = user.Id;
                nombreCompleto = nombreCompleto + " " + user.LastNames;
                var memberShip = db.aspnet_Membership.Find(idUsuario);
                selected.Add(memberShip.Email, nombreCompleto);
            }

            TempData["subject"] = asunto;
            TempData["message"] = mensaje;
            TempData["d"] = selected;

            return RedirectToAction("Preview", new { id });
        }

        [HttpPost]
        public JsonResult BuscarPorNombre(string words)
        {
            var suggestions = from u in db.Users select u.FirstNames;
            var namelist = suggestions.Where(u => u.ToLower().StartsWith(words.ToLower()));
            return Json(namelist, JsonRequestBehavior.AllowGet);
        }

    }
}
