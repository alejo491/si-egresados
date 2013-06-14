using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ClientServices.Providers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using AplicacionBase.Controllers;
using AplicacionBase.Models;
using FluentSecurity;
using FluentSecurity.Policy;

namespace AplicacionBase
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Nombre de ruta
                "{controller}/{action}/{id}", // URL con parámetros
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Valores predeterminados de parámetro
            );

        }

        protected void Application_Start()
        {


            ////Cargar todos los controladores y metodos automaticamente, no se cargan los metodos si ya estan en la base de datos.
            //#region Cargar controladores
            //var assembly = new AssemblyHelper();
            //var result = assembly.GetControllerNames();
            //#endregion

            //#region Reglas de seguridad
            ////Reglas de configuración
            //SecurityConfigurator.Configure(configuration =>
            //{
            //    if (result)
            //    {
            //        //Se le dice a la aplicacion de donde sacar el estado del usuario y los roles que tiene
            //        configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
            //        configuration.GetRolesFrom(Roles.GetRolesForUser);

            //        //Reglas de configruacion para controladores

            //        #region Reglas del Home Controller
            //            //Configuracion para el controlador de Home
            //            configuration.For<HomeController>().Ignore();
            //            configuration.For<VerifyController>().Ignore();
            //            configuration.For<AccountController>().Ignore();

            //        #endregion
     
            //        //****************************************************************************************************//
            //        //*********************¡AQUI VAN TODAS LAS REGLAS QUE SE VAN A AGREGAR! *******************************//
            //        //****************************************************************************************************//

            //        #region Reglas de configuracion por cada controlador

            //        //************Configuracion para el controlador de encuestas.********************

            //            #region Reglas del controlador Surveys   
            //                //Se obtienen los roles que tienen acceso al controlador Surveys y a los metodos de este controlador
            //                string[] rolesSurveysIndex = assembly.GetRolesMethods("SurveysController", "Index");
            //                var rolesSurveysCreate = assembly.GetRolesMethods("SurveysController", "Create");
            //                var rolesSurveysEdit = assembly.GetRolesMethods("SurveysController", "Edit");
            //                var rolesSurveysDelete = assembly.GetRolesMethods("SurveysController", "Delete");
            //                var rolesSurveysDeleteConfirmed = assembly.GetRolesMethods("SurveysController", "DeleteConfirmed");
            //                var rolesSurveysDetails = assembly.GetRolesMethods("SurveysController", "Detail");


            //                //Se configuran las reglas de acceso para el controlador surveys.
            //                configuration.For<SurveysController>(x => x.Index())
            //                             .RequireAnyRole(rolesSurveysIndex);

            //                configuration.For<SurveysController>(x => x.Create())
            //                             .RequireAnyRole(rolesSurveysCreate);

            //                configuration.For<SurveysController>(x => x.Edit(default(Guid)))
            //                             .RequireAnyRole(rolesSurveysEdit);

            //                configuration.For<SurveysController>(x => x.Delete(default(Guid)))
            //                             .RequireAnyRole(rolesSurveysDelete);

            //                configuration.For<SurveysController>(x => x.DeleteConfirmed(default(Guid)))
            //                              .RequireAnyRole(rolesSurveysDeleteConfirmed);

            //                configuration.For<SurveysController>(x => x.Details(default(Guid)))
            //                              .RequireAnyRole(rolesSurveysDetails);

            //            #endregion
                        
            //        #endregion

            //        //****************************************************************************************************//
            //        //****************************************************************************************************//
            //        //****************************************************************************************************//
            //    }



            //});
            //#endregion
            
            
            ////Se añaden las reglas
            //GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);
            //GlobalFilters.Filters.Add(new DenyAnonymousAccessPolicyViolationHandler());
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            CreateSteps();

        }

        /*Lineas Necesarias, para Administrar el Wizard*/
        protected void Session_Start(object sender, EventArgs e)
        {
            /*Variable de Sesion para el Wizard*/

            Session["Wizard"] = "0";
            Session["User"] = string.Empty;
            Session["steps"] = null;
            Session["firstTime"] = false;
            /*--------------------------------*/

        }

        /*Lineas Necesarias, para Administrar el Wizard*/
        /// <summary>
        /// Se Crean los pasos del Wizard
        /// </summary>
        private void CreateSteps()
        {
            var db = new DbSIEPISContext();
            var steps = db.Steps;
            if (steps.Any()) return;
            var survey = new Survey
            {
                Id = Guid.NewGuid(),
                Name = "Información Adicional",
                Aim = "Reunir información adicional de los usuarios"
            };
            db.Surveys.Add(survey);
            db.SaveChanges();
            var count = 0;
            while (count < 3)
            {
                var obj = new Step
                {
                    Id = Guid.NewGuid(),
                    SOrder = count
                };
                switch (count)
                {
                   
                    case 0:
                        obj.SPath = @"/Study/Index?wizardStep=1";
                        break;
                    case 1:
                        obj.SPath = @"/Experiences/Index?wizardStep=1";
                        break;
                    case 2:
                        //obj.SPath = @"/Surveys/Index";
                        obj.SPath = @"/FillSurvey/Fill?ids=" + survey.Id.ToString() + @"&wizardStep=1";
                        break;
                }

                db.Steps.Add(obj);
                db.SaveChanges();
                count++;
            }

        }
    }

}