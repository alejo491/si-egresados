﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionBase.Controllers
{
   

    public class SendSurveysController : Controller
    {

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

        //
        // GET: /SendSurveys/

        public ActionResult Send(Guid id)
        {
            return View();
           // return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public ActionResult Send(FormCollection form)
        {
            var selected= new Dictionary<string,string>();

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

            return View();
            //return RedirectToAction("Index", "Home");
        }

    }
}
