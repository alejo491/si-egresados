using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ClientServices.Providers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using AplicacionBase.Controllers;

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
            
            /*var assembly = new AssemblyHelper();
            var result = assembly.GetControllerNames();
            SecurityConfigurator.Configure(configuration =>
            {
                if (result)
                {
                    configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                    configuration.GetRolesFrom(Roles.GetRolesForUser);
                    configuration.For<HomeController>(x => x.Index())
                                 .RequireRole(assembly.GetRolesMethods("HomeController", "Index"));
                    configuration.For<VerifyController>().Ignore();
                    configuration.For<AccountController>().Ignore();
                    configuration.For<UserController>().Ignore();
                    configuration.For<UsersRolesController>().Ignore();
                    var rolesAssign = assembly.GetRolesMethods("RoleMethodsController", "AssignRolesMethods");
                    if (rolesAssign.Length > 0)
                    {
                        configuration.For<RoleMethodsController>(x => x.AssignRolesMethods(Guid.NewGuid()))
                                     .RequireRole(rolesAssign);
                    }
                    else
                    {
                        configuration.For<RoleMethodsController>(x => x.AssignRolesMethods(Guid.NewGuid()))
                                     .Ignore();
                    }
                }



            });
            
            GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);*/
            //GlobalFilters.Filters.Add(new DenyAnonymousAccessPolicyViolationHandler());
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

        }
    }
}