using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
using System.Diagnostics;

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

        //
        // GET: /SendSurveys/

        public ActionResult Send(Guid id)
        {
            return View();
           // return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public ActionResult Send(Guid id, FormCollection form)
        {
            var selected= new Dictionary<string,string>();
            idObtenido = id;
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
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Sistemas", Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria Automatica", Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Ingenieria de Electronica", Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosJefe("Telematica", Convert.ToDateTime(fechaDesde));
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
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas", Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (ingenieriaAutomatica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica", Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (ingenieriaElectronica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica", Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                        else if (telematica)
                        {
                            var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica", Convert.ToDateTime(fechaDesde));
                            selected.Concat(c);
                        }
                    }
                }

            if (egresados != true && jefe != true)
            {
                if (ingenieriadeSistemas)
                {
                    var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Sistemas", Convert.ToDateTime(fechaDesde));
                    selected.Concat(c);
                }
                else if (ingenieriaAutomatica)
                {
                    var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria Automatica", Convert.ToDateTime(fechaDesde));
                    selected.Concat(c);
                }
                else if (ingenieriaElectronica)
                {
                    var c = SendSurveysDbController.ListarEgresadosPrograma("Ingenieria de Electronica", Convert.ToDateTime(fechaDesde));
                    selected.Concat(c);
                }
                else if (telematica)
                {
                    var c = SendSurveysDbController.ListarEgresadosPrograma("Telematica", Convert.ToDateTime(fechaDesde));
                    selected.Concat(c);
                }
            }

            return RedirectToAction("Preview", new { id });
            //return RedirectToAction("Index", "Home");
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
                var memberShip = db.aspnet_Membership.Find(idUsuario);
                selected.Add(memberShip.Email, nombreCompleto);
            }

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
