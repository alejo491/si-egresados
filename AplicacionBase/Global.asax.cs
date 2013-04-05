using System;
using System.Web;
using System.Web.ClientServices.Providers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using AplicacionBase.Controllers;
using FluentSecurity;

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
            AreaRegistration.RegisterAllAreas();            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            /*SecurityConfigurator.Configure(configuration =>
            {
                // Let FluentSecurity know how to get the authentication status of the current user
                configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                configuration.GetRolesFrom(Roles.GetAllRoles);
                // This is where you set up the policies you want FluentSecurity to enforce on your controllers and actions
                configuration.For<HomeController>().Ignore();
                configuration.For<AccountController>().DenyAuthenticatedAccess();
                configuration.For<AccountController>(x => x.ChangePassword()).DenyAnonymousAccess();
                configuration.For<AccountController>(x => x.LogOff()).DenyAnonymousAccess();
                var asd = Roles.GetAllRoles();
                TopicController t = new TopicController();
                
                configuration.For<TopicController>().RequireAllRoles(asd);
            });

            GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);*/
        }
    }
}