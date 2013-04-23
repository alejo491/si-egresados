using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
   

    public class SendSurveysController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        private bool estudiante;
        private bool docentes;
        private bool egresados;
        private bool administrativos;
        private bool jefe;
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

        //
        // GET: /SendSurveys/

        public ActionResult Send(Guid id)
        {
            ViewBag.Seleccionado1 = false;
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
            ViewBag.Seleccionado1 = false;

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
                    case "Estudiantes":
                        continuar = k.Contains("true");
                        if (continuar)
                        {
                            ViewBag.Seleccionado1 = true;
                        }
                        else
                        {
                            ViewBag.Seleccionado1 = false;
                        }
                        break;
                    case "Docentes":
                        continuar = k.Contains("true");
                        ViewBag.Seleccionado2 = v;
                        break;
                    case "Egresados":
                        continuar = k.Contains("true");
                        ViewBag.Seleccionado3 = v;
                        break;
                    case "Administrativos":
                        continuar = k.Contains("true");
                        ViewBag.Seleccionado4 = v;
                        break;
                }
            }

            
            if (continuar == false)
            {
                TempData["Error1"] = "Debes seleccionar al menos un destinatario";
               // return View();
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
                        //    return View();
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

                foreach (string variable in form)
                {
                    var k = form[variable];
                    switch (variable)
                    {
                        case "Estudiantes":
                            estudiante = k.Contains("true");
                            break;
                        case "Docentes":
                            docentes = k.Contains("true");
                            break;
                        case "Egresados":
                            egresados = k.Contains("true");
                            break;
                        case "Administrativos":
                            administrativos = k.Contains("true");
                            break;
                        case "Jefe":
                            jefe = k.Contains("true");
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
                if (jefe)
                {
                    if (fechaDesde == "")
                    {
                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas");
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica");
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica");
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica");
                            selected.Concat(c);
                        }
                    }
                    else
                    {
                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas",
                                                                                Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica",
                                                                                Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica",
                                                                                Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica",
                                                                                Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                    }
                }

                if (egresados)
                {
                    if (fechaDesde == "")
                    {
                        if (ingenieriadeSistemas)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas");
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica");
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica");
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica");
                            selected.Concat(c);
                        }
                    }
                    else
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
                }

                if (egresados != true && jefe != true)
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

                TempData["message"] = mensaje;
                TempData["subject"] = asunto;
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
