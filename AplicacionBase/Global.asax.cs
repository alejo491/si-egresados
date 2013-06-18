using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

            if (false)
            {
            //Cargar todos los controladores y metodos automaticamente, no se cargan los metodos si ya estan en la base de datos.
            #region Cargar controladores
            var assembly = new AssemblyHelper();
            var result = assembly.GetControllerNames();
            #endregion

            
            

                #region Reglas de seguridad
            //Reglas de configuración
            SecurityConfigurator.Configure(configuration =>
            {
                
                    //Se le dice a la aplicacion de donde sacar el estado del usuario y los roles que tiene
                    configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                    configuration.GetRolesFrom(Roles.GetRolesForUser);

                    //Reglas de configruacion para controladores

                    #region Reglas del Home Controller
                    //Configuracion para el controlador de Home
                    configuration.For<HomeController>().Ignore();
                    configuration.For<VerifyController>().Ignore();
                    configuration.For<AccountController>().Ignore();

                    #endregion

                    //****************************************************************************************************//
                    //*********************¡AQUI VAN TODAS LAS REGLAS QUE SE VAN A AGREGAR! *******************************//
                    //****************************************************************************************************//

                    #region Reglas de configuracion por cada controlador

                    //************Configuracion para el controlador de encuestas.********************

                    #region Reglas del controlador Surveys
                    //Se obtienen los roles que tienen acceso al controlador Surveys y a los metodos de este controlador
                    string[] rolesSurveysIndex = assembly.GetRolesMethods("SurveysController", "Index");
                    var rolesSurveysCreate = assembly.GetRolesMethods("SurveysController", "Create");
                    var rolesSurveysEdit = assembly.GetRolesMethods("SurveysController", "Edit");
                    var rolesSurveysDelete = assembly.GetRolesMethods("SurveysController", "Delete");
                    var rolesSurveysDeleteConfirmed = assembly.GetRolesMethods("SurveysController", "DeleteConfirmed");
                    var rolesSurveysDetails = assembly.GetRolesMethods("SurveysController", "Detail");


                    //Se configuran las reglas de acceso para el controlador surveys.
                    configuration.For<SurveysController>(x => x.Index())
                                 .RequireAnyRole(rolesSurveysIndex);

                    configuration.For<SurveysController>(x => x.Create())
                                 .RequireAnyRole(rolesSurveysCreate);

                    configuration.For<SurveysController>(x => x.Edit(default(Guid)))
                                 .RequireAnyRole(rolesSurveysEdit);

                    configuration.For<SurveysController>(x => x.Delete(default(Guid)))
                                 .RequireAnyRole(rolesSurveysDelete);

                    configuration.For<SurveysController>(x => x.DeleteConfirmed(default(Guid)))
                                  .RequireAnyRole(rolesSurveysDeleteConfirmed);

                    configuration.For<SurveysController>(x => x.Details(default(Guid)))
                                  .RequireAnyRole(rolesSurveysDetails);

                    #endregion

                    #region Reglas del controlador Post
                    //Se obtienen los roles que tienen acceso al controlador Post y a los métodos de este controlador.
                    string[] rolesPostIndex = assembly.GetRolesMethods("PostController", "Index");
                    string[] rolesPostIndexpublic = assembly.GetRolesMethods("PostController", "Indexpublic");
                    var rolesPostCreate = assembly.GetRolesMethods("PostController", "Create");
                    var rolesPostDetails = assembly.GetRolesMethods("PostController", "Detail");
                    var rolesPostEdit = assembly.GetRolesMethods("PostController", "Edit");
                    var rolesPostDelete = assembly.GetRolesMethods("PostController", "Delete");
                    var rolesPostDeleteConfirmed = assembly.GetRolesMethods("PostController", "DeleteConfirmed");
                    var rolesPostShowPosts = assembly.GetRolesMethods("PostController", "ShowPosts");
                    var rolesPostShowPost = assembly.GetRolesMethods("PostController", "ShowPost");

                    //Se configuran las reglas de acceso para el controlador Post.
                    configuration.For<PostController>(x => x.Index())
                                 .RequireAnyRole(rolesPostIndex);

                    configuration.For<PostController>(x => x.Indexpublic())
                                 .RequireAnyRole(rolesPostIndexpublic);

                    configuration.For<PostController>(x => x.Create())
                                 .RequireAnyRole(rolesPostCreate);

                    configuration.For<PostController>(x => x.Details(default(Guid)))
                                  .RequireAnyRole(rolesPostDetails);

                    configuration.For<PostController>(x => x.Edit(default(Guid)))
                                 .RequireAnyRole(rolesPostEdit);

                    configuration.For<PostController>(x => x.Delete(default(Guid)))
                                 .RequireAnyRole(rolesPostDelete);

                    configuration.For<PostController>(x => x.DeleteConfirmed(default(Guid)))
                                 .RequireAnyRole(rolesPostDeleteConfirmed);

                    configuration.For<PostController>(x => x.ShowPosts())
                                 .RequireAnyRole(rolesPostShowPosts);

                    configuration.For<PostController>(x => x.ShowPost(default(Guid)))
                                 .RequireAnyRole(rolesPostShowPost);

                    #endregion

                    #region Reglas del controlador Like
                    // Se obtienen los roles que tienen acceso al controlador Like y a los métodos de este controlador.
                    string[] rolesLikeIndex = assembly.GetRolesMethods("LikeController", "Index");
                    var rolesLikeCreate = assembly.GetRolesMethods("LikeController", "Create");
                    var rolesLikeDetails = assembly.GetRolesMethods("LikeController", "Detail");
                    var rolesLikeEdit = assembly.GetRolesMethods("LikeController", "Edit");
                    var rolesLikeDelete = assembly.GetRolesMethods("LikeController", "Delete");
                    var rolesLikeDeleteConfirmed = assembly.GetRolesMethods("LikeController", "DeleteConfirmed");

                    //Se configuran las reglas de acceso para el controlador Like.
                    configuration.For<LikeController>(x => x.Index())
                                 .RequireAnyRole(rolesLikeIndex);

                    configuration.For<LikeController>(x => x.Create(default(Guid)))
                                 .RequireAnyRole(rolesLikeCreate);

                    configuration.For<LikeController>(x => x.Details(default(Guid)))
                                  .RequireAnyRole(rolesLikeDetails);

                    configuration.For<LikeController>(x => x.Edit(default(Guid)))
                                 .RequireAnyRole(rolesLikeEdit);

                    configuration.For<LikeController>(x => x.Delete(default(Guid)))
                                 .RequireAnyRole(rolesLikeDelete);

                    configuration.For<LikeController>(x => x.DeleteConfirmed(default(Guid), default(Guid)))
                                 .RequireAnyRole(rolesLikeDeleteConfirmed);

                    #endregion

                    #region Reglas del controlador Startbox
                    //Se obtienen los roles que tienen acceso al controlador Startbox y a los métodos de este controlador.
                    string[] rolesStartboxIndex = assembly.GetRolesMethods("StartboxController", "Index");
                    var rolesStartboxCreate = assembly.GetRolesMethods("StartboxController", "Create");
                    var rolesStartboxDetails = assembly.GetRolesMethods("StartboxController", "Detail");
                    var rolesStartboxEdit = assembly.GetRolesMethods("StartboxController", "Edit");
                    var rolesStartboxDelete = assembly.GetRolesMethods("StartboxController", "Delete");
                    var rolesStartboxDeleteConfirmed = assembly.GetRolesMethods("StartboxController", "DeleteConfirmed");

                    //Se configuran las reglas de acceso para el controlador Startbox.
                    configuration.For<StartboxController>(x => x.Index())
                                 .RequireAnyRole(rolesStartboxIndex);

                    configuration.For<StartboxController>(x => x.Create())
                                 .RequireAnyRole(rolesStartboxCreate);

                    configuration.For<StartboxController>(x => x.Details(default(Guid)))
                                  .RequireAnyRole(rolesStartboxDetails);

                    configuration.For<StartboxController>(x => x.Edit(default(Guid)))
                                 .RequireAnyRole(rolesStartboxEdit);

                    configuration.For<StartboxController>(x => x.Delete(default(Guid)))
                                 .RequireAnyRole(rolesStartboxDelete);

                    configuration.For<StartboxController>(x => x.DeleteConfirmed(default(Guid)))
                                 .RequireAnyRole(rolesStartboxDeleteConfirmed);

                    #endregion

                    #region Reglas del controlador AnswerChoices
                    //Se obtienen los roles que tienen acceso al controlador AnswerChoices y a los metodos de este controlador
                    string[] rolesAnswerChoicesIndex = assembly.GetRolesMethods("AnswerChoicesController", "Index");
                    var rolesAnswerChoicesCreate = assembly.GetRolesMethods("AnswerChoicesController", "Create");
                    var rolesAnswerChoicesEdit = assembly.GetRolesMethods("AnswerChoicesController", "Edit");
                    var rolesAnswerChoicesDelete = assembly.GetRolesMethods("AnswerChoicesController", "Delete");
                    var rolesAnswerChoicesDeleteConfirmed = assembly.GetRolesMethods("SurveysController", "DeleteConfirmed");
                    var rolesAnswerChoicesDetails = assembly.GetRolesMethods("AnswerChoicesController", "Detail");


                    //Se configuran las reglas de acceso para el controlador AnswerChoices.
                    configuration.For<AnswerChoicesController>(x => x.Index(default(Guid)))
                                 .RequireAnyRole(rolesAnswerChoicesIndex);

                    configuration.For<AnswerChoicesController>(x => x.Create(default(Guid)))
                                 .RequireAnyRole(rolesAnswerChoicesCreate);

                    configuration.For<AnswerChoicesController>(x => x.Edit(default(Guid)))
                                 .RequireAnyRole(rolesAnswerChoicesEdit);

                    configuration.For<AnswerChoicesController>(x => x.Delete(default(Guid)))
                                 .RequireAnyRole(rolesAnswerChoicesDelete);

                    configuration.For<AnswerChoicesController>(x => x.DeleteConfirmed(default(Guid)))
                                  .RequireAnyRole(rolesAnswerChoicesDeleteConfirmed);

                    configuration.For<AnswerChoicesController>(x => x.Details(default(Guid)))
                                  .RequireAnyRole(rolesAnswerChoicesDetails);

                    #endregion

                    #region Reglas del controlador FillSurvey
                    //Se obtienen los roles que tienen acceso al controlador FillSurvey y a los metodos de este controlador
                    string[] rolesFillSurveyFill = assembly.GetRolesMethods("FillSurveyController", "Fill");


                    //Se configuran las reglas de acceso para el controlador FillSurvey.
                    configuration.For<FillSurveyController>(x => x.Fill(default(Guid), default(string)))
                        .Ignore();

                    #endregion

                    #region Reglas del controlador ItemData
                    //Se obtienen los roles que tienen acceso al controlador ItemData y a los metodos de este controlador
                    string[] rolesItemDataCreate = assembly.GetRolesMethods("ItemDataController", "Create");
                    var rolesItemDataDelete = assembly.GetRolesMethods("AnswerChoicesController", "Delete");
                    var rolesItemDataDeleteConfirmed = assembly.GetRolesMethods("SurveysController", "DeleteConfirmed");


                    //Se configuran las reglas de acceso para el controlador ItemData.

                    configuration.For<ItemDataController>(x => x.Create(default(Guid)))
                                 .RequireAnyRole(rolesItemDataCreate);

                    configuration.For<ItemDataController>(x => x.Delete(default(Guid)))
                                 .RequireAnyRole(rolesItemDataDelete);

                    configuration.For<ItemDataController>(x => x.DeleteConfirmed(default(Guid)))
                                  .RequireAnyRole(rolesItemDataDeleteConfirmed);

                    #endregion

                    #region Reglas del controlador ItemSurveys
                    //Se obtienen los roles que tienen acceso al controlador ItemSurveys y a los metodos de este controlador
                    string[] rolesItemSurveysIndex = assembly.GetRolesMethods("ItemSurveysController", "Index");
                    var rolesItemSurveysCreate = assembly.GetRolesMethods("ItemSurveysController", "Create");
                    var rolesItemSurveysDelete = assembly.GetRolesMethods("ItemSurveysController", "Delete");
                    var rolesItemSurveysDeleteConfirmed = assembly.GetRolesMethods("ItemSurveysController", "DeleteConfirmed");
                    var rolesItemSurveysDetails = assembly.GetRolesMethods("ItemSurveysController", "Detail");


                    //Se configuran las reglas de acceso para el controlador ItemSurveys.
                    configuration.For<ItemSurveysController>(x => x.Index(default(Guid)))
                                 .RequireAnyRole(rolesItemSurveysIndex);

                    configuration.For<ItemSurveysController>(x => x.Create(default(Guid)))
                                 .RequireAnyRole(rolesItemSurveysCreate);

                    configuration.For<ItemSurveysController>(x => x.Delete(default(Guid)))
                                 .RequireAnyRole(rolesItemSurveysDelete);

                    configuration.For<ItemSurveysController>(x => x.DeleteConfirmed(default(Guid)))
                                  .RequireAnyRole(rolesItemSurveysDeleteConfirmed);

                    configuration.For<ItemSurveysController>(x => x.Details(default(Guid)))
                                  .RequireAnyRole(rolesItemSurveysDetails);

                    #endregion

                    #region Reglas del controlador Aspnet_Roles
                    //Se obtienen los roles que tienen acceso al controlador Aspnet_Roles y a los metodos de este controlador
                    string[] rolesAspnet_RolesIndex = assembly.GetRolesMethods("Aspnet_RolesController", "Index");
                    var rolesAspnet_RolesCreate = assembly.GetRolesMethods("Aspnet_RolesController", "Create");
                    var rolesAspnet_RolesEdit = assembly.GetRolesMethods("Aspnet_RolesController", "Edit");
                    var rolesAspnet_RolesDelete = assembly.GetRolesMethods("Aspnet_RolesController", "Delete");
                    var rolesAspnet_RolesDeleteConfirmed = assembly.GetRolesMethods("Aspnet_RolesController", "DeleteConfirmed");
                    var rolesAspnet_RolesDetails = assembly.GetRolesMethods("Aspnet_RolesController", "Detail");


                    //Se configuran las reglas de acceso para el controlador Aspnet_Roles.
                    configuration.For<Aspnet_RolesController>(x => x.Index())
                                 .RequireAnyRole(rolesAspnet_RolesIndex);

                    configuration.For<Aspnet_RolesController>(x => x.Create())
                                 .RequireAnyRole(rolesAspnet_RolesCreate);

                    configuration.For<Aspnet_RolesController>(x => x.Edit(default(Guid)))
                                 .RequireAnyRole(rolesAspnet_RolesEdit);

                    configuration.For<Aspnet_RolesController>(x => x.Delete(default(Guid)))
                                 .RequireAnyRole(rolesAspnet_RolesDelete);

                    configuration.For<Aspnet_RolesController>(x => x.DeleteConfirmed(default(Guid)))
                                  .RequireAnyRole(rolesAspnet_RolesDeleteConfirmed);

                    configuration.For<Aspnet_RolesController>(x => x.Details(default(Guid)))
                                  .RequireAnyRole(rolesAspnet_RolesDetails);

                    #endregion

                    #region Reglas del controlador Questions
                    //Se obtienen los roles que tienen acceso al controlador Questions y a los metodos de este controlador
                    string[] rolesQuestionsIndex = assembly.GetRolesMethods("QuestionsController", "Index");
                    var rolesQuestionsCreate = assembly.GetRolesMethods("QuestionsController", "Create");
                    var rolesQuestionsEdit = assembly.GetRolesMethods("QuestionsController", "Edit");
                    var rolesQuestionsDelete = assembly.GetRolesMethods("QuestionsController", "Delete");
                    var rolesQuestionsDeleteConfirmed = assembly.GetRolesMethods("QuestionsController", "DeleteConfirmed");
                    var rolesQuestionsDetails = assembly.GetRolesMethods("QuestionsController", "Details");


                    //Se configuran las reglas de acceso para el controlador Questions.
                    configuration.For<QuestionsController>(x => x.Index(default(Guid)))
                    .RequireAnyRole(rolesQuestionsIndex);

                    configuration.For<QuestionsController>(x => x.Create(default(Guid)))
                    .RequireAnyRole(rolesQuestionsCreate);

                    configuration.For<QuestionsController>(x => x.Edit(default(Guid), default(Guid)))
                    .RequireAnyRole(rolesQuestionsEdit);

                    configuration.For<QuestionsController>(x => x.Delete(default(Guid)))
                    .RequireAnyRole(rolesQuestionsDelete);

                    configuration.For<QuestionsController>(x => x.DeleteConfirmed(default(Guid)))
                    .RequireAnyRole(rolesQuestionsDeleteConfirmed);

                    configuration.For<QuestionsController>(x => x.Details(default(Guid)))
                    .RequireAnyRole(rolesQuestionsDetails);

                    #endregion

                    #region Reglas del controlador Wizard
                    //Se obtienen los roles que tienen acceso al controlador Wizard y a los metodos de este controlador
                    string[] rolesWizardIndex = assembly.GetRolesMethods("WizardController", "Index");
                    /*var rolesWizardSkip = assembly.GetRolesMethods("WizardController", "Skip");
                    var rolesWizardNext = assembly.GetRolesMethods("WizardController", "Next");
                    var rolesWizardEnd = assembly.GetRolesMethods("WizardController", "End");
                    */

                    //Se configuran las reglas de acceso para el controlador Wizard.
                    configuration.For<WizardController>(x => x.Index(0))
                    .RequireAnyRole(rolesWizardIndex);


                    #endregion

                    #region Reglas del controlador SendSurveys
                    //Se obtienen los roles que tienen acceso al controlador SendSurveys y a los metodos de este controlador
                    string[] rolesSendSurveysSend = assembly.GetRolesMethods("SendSurveysController", "Send");
                    string[] rolesSendSurveysSendSpecific = assembly.GetRolesMethods("SendSurveysController", "SendSpecific");
                    string[] rolesSendSurveysPreview = assembly.GetRolesMethods("SendSurveysController", "Preview");

                    //Se configuran las reglas de acceso para el controlador SendSurveys.
                    configuration.For<SendSurveysController>(x => x.Send(default(Guid)))
                    .RequireAnyRole(rolesSendSurveysSend);

                    configuration.For<SendSurveysController>(x => x.SendSpecific(default(Guid)))
                                    .RequireAnyRole(rolesSendSurveysSendSpecific);

                    configuration.For<SendSurveysController>(x => x.Preview(default(Guid)))
                                                    .RequireAnyRole(rolesSendSurveysPreview);


                    #endregion

                    #region Reglas del controlador Topic
                    //Se obtienen los roles que tienen acceso al controlador topic y a los metodos de este controlador
                    string[] rolesTopicIndex = assembly.GetRolesMethods("TopicController", "Index");
                    var rolesTopicCreate = assembly.GetRolesMethods("TopicController", "Create");
                    var rolesTopicEdit = assembly.GetRolesMethods("TopicController", "Edit");
                    var rolesTopicDelete = assembly.GetRolesMethods("TopicController", "Delete");
                    var rolesTopicDeleteConfirmed = assembly.GetRolesMethods("TopicController", "DeleteConfirmed");
                    var rolesTopicDetails = assembly.GetRolesMethods("TopicController", "Detail");


                    //Se configuran las reglas de acceso para el controlador topic.
                    configuration.For<TopicController>(x => x.Index())
                        .RequireAnyRole(rolesTopicIndex);

                    configuration.For<TopicController>(x => x.Create())
                        .RequireAnyRole(rolesTopicCreate);

                    configuration.For<TopicController>(x => x.Edit(default(Guid)))
                        .RequireAnyRole(rolesTopicEdit);

                    configuration.For<TopicController>(x => x.Delete(default(Guid)))
                        .RequireAnyRole(rolesTopicDelete);

                    configuration.For<TopicController>(x => x.DeleteConfirmed(default(Guid)))
                        .RequireAnyRole(rolesTopicDeleteConfirmed);

                    configuration.For<TopicController>(x => x.Details(default(Guid)))
                        .RequireAnyRole(rolesTopicDetails);

                    #endregion

                    #region Reglas del controlador UserController
                    //Se obtienen los roles que tienen acceso al controlador User y a los metodos de este controlador
                    string[] rolesUserIndex = assembly.GetRolesMethods("UserController", "Index");
                    var rolesUserCreate = assembly.GetRolesMethods("UserController", "Create");
                    var rolesUserBegin = assembly.GetRolesMethods("UserController", "Begin");
                    var rolesUserEdit = assembly.GetRolesMethods("UserController", "Edit");
                    var rolesUserState = assembly.GetRolesMethods("UserController", "State");
                    var rolesUserGenerate = assembly.GetRolesMethods("UserController", "Generate");
                    var rolesUserDetails = assembly.GetRolesMethods("UserController", "Details");
                    var rolesUserRegister = assembly.GetRolesMethods("UserController", "Register");
                    var rolesUserSearch = assembly.GetRolesMethods("UserController", "Search");

                    //Se configuran las reglas de acceso para el controlador User.
                    configuration.For<UserController>(x => x.Index(0)).RequireAnyRole(rolesUserIndex);

                    configuration.For<UserController>(x => x.Create()).RequireAnyRole(rolesUserCreate);

                    configuration.For<UserController>(x => x.Begin(default(Guid))).RequireAnyRole(rolesUserBegin);

                    configuration.For<UserController>(x => x.Edit(default(Guid))).RequireAnyRole(rolesUserEdit);

                    configuration.For<UserController>(x => x.State(default(Guid))).RequireAnyRole(rolesUserState);

                    configuration.For<UserController>(x => x.Generate(default(Guid))).RequireAnyRole(rolesUserGenerate);

                    configuration.For<UserController>(x => x.Details(default(Guid))).RequireAnyRole(rolesUserDetails);

                    configuration.For<UserController>(x => x.Register()).RequireAnyRole(rolesUserRegister);

                    configuration.For<UserController>(x => x.Search(default(string), 0)).RequireAnyRole(rolesUserSearch);

                    #endregion

                    #region Reglas del controlador ElectiveController
                    //Se obtienen los roles que tienen acceso al controlador  Elective y a los metodos de este controlador
                    string[] rolesElectiveIndex = assembly.GetRolesMethods("ElectiveController", "Index");
                    var rolesElectiveCreate = assembly.GetRolesMethods("ElectiveController", "Create");
                    var rolesElectiveEdit = assembly.GetRolesMethods("ElectiveController", "Edit");
                    var rolesElectiveDelete = assembly.GetRolesMethods("ElectiveController", "Delete");
                    var rolesElectiveDeleteConfirmed = assembly.GetRolesMethods("ElectiveController", "DeleteConfirmed");
                    var rolesElectiveDetails = assembly.GetRolesMethods("ElectiveController", "Detail");


                    //Se configuran las reglas de acceso para el controlador Elective.
                    configuration.For<ElectiveController>(x => x.Index()).RequireAnyRole(rolesElectiveIndex);

                    //configuration.For<ElectiveController>(x => x.Create()).RequireAnyRole(rolesElectiveCreate);

                    configuration.For<ElectiveController>(x => x.Edit(default(Guid))).RequireAnyRole(rolesElectiveEdit);

                    //configuration.For<ElectiveController>(x => x.Delete(default(Guid))).RequireAnyRole(rolesElectiveDelete);

                    //configuration.For<ElectiveController>(x => x.DeleteConfirmed(default(Guid))).RequireAnyRole(rolesElectiveDeleteConfirmed);

                    configuration.For<ElectiveController>(x => x.Details(default(Guid))).RequireAnyRole(rolesElectiveDetails);

                    #endregion

                    #region Reglas del controlador SchoolController
                    //Se obtienen los roles que tienen acceso al controlador School y a los metodos de este controlador
                    string[] rolesSchoolIndex = assembly.GetRolesMethods("SchoolController", "Index");
                    var rolesSchoolCreate = assembly.GetRolesMethods("SchoolController", "Create");
                    var rolesSchoolEdit = assembly.GetRolesMethods("SchoolController", "Edit");
                    var rolesSchoolDelete = assembly.GetRolesMethods("SchoolController", "Delete");
                    var rolesSchoolDeleteConfirmed = assembly.GetRolesMethods("SchoolController", "DeleteConfirmed");
                    var rolesSchoolDetails = assembly.GetRolesMethods("SchoolController", "Detail");


                    //Se configuran las reglas de acceso para el controlador School.
                    configuration.For<SchoolController>(x => x.Index()).RequireAnyRole(rolesSchoolIndex);

                    //configuration.For<SchoolController>(x => x.Create()).RequireAnyRole(rolesSchoolCreate);

                    configuration.For<SchoolController>(x => x.Edit(default(Guid))).RequireAnyRole(rolesSchoolEdit);

                    //configuration.For<SchoolController>(x => x.Delete(default(Guid))).RequireAnyRole(rolesSchoolDelete);

                    //configuration.For<SchoolController>(x => x.DeleteConfirmed(default(Guid))).RequireAnyRole(rolesSchoolDeleteConfirmed);

                    configuration.For<SchoolController>(x => x.Details(default(Guid))).RequireAnyRole(rolesSchoolDetails);

                    #endregion

                    #region Reglas del controlador StudyController
                    //Se obtienen los roles que tienen acceso al controlador Study y a los metodos de este controlador
                    string[] rolesStudyIndex = assembly.GetRolesMethods("StudyController", "Index");
                    var rolesStudyCreate = assembly.GetRolesMethods("StudyController", "Create");
                    var rolesStudyEdit = assembly.GetRolesMethods("StudyController", "Edit");
                    var rolesStudyDelete = assembly.GetRolesMethods("StudyController", "Delete");
                    var rolesStudyDeleteConfirmed = assembly.GetRolesMethods("StudyController", "DeleteConfirmed");
                    var rolesStudyDetails = assembly.GetRolesMethods("StudyController", "Detail");


                    //Se configuran las reglas de acceso para el controlador Study.
                    configuration.For<StudyController>(x => x.Index(default(Guid), 0)).RequireAnyRole(rolesStudyIndex);

                    configuration.For<StudyController>(x => x.Create(default(Guid), 0)).RequireAnyRole(rolesStudyCreate);

                    configuration.For<StudyController>(x => x.Edit(default(Guid), default(Guid), 0)).RequireAnyRole(rolesStudyEdit);

                    configuration.For<StudyController>(x => x.Delete(default(Guid), default(Guid), 0)).RequireAnyRole(rolesStudyDelete);

                    configuration.For<StudyController>(x => x.DeleteConfirmed(default(Guid), default(Guid))).RequireAnyRole(rolesStudyDeleteConfirmed);

                    configuration.For<StudyController>(x => x.Details(default(Guid), default(Guid), 0)).RequireAnyRole(rolesStudyDetails);

                    #endregion

                    #region Reglas del controlador ThesisController
                    //Se obtienen los roles que tienen acceso al controlador Thesis y a los metodos de este controlador
                    string[] rolesThesisIndex = assembly.GetRolesMethods("ThesisController", "Index");
                    var rolesThesisCreate = assembly.GetRolesMethods("ThesisController", "Create");
                    var rolesThesisEdit = assembly.GetRolesMethods("ThesisController", "Edit");
                    var rolesThesisDelete = assembly.GetRolesMethods("ThesisController", "Delete");
                    var rolesThesiseleteConfirmed = assembly.GetRolesMethods("ThesisController", "DeleteConfirmed");
                    var rolesThesisDetails = assembly.GetRolesMethods("ThesisController", "Detail");


                    //Se configuran las reglas de acceso para el controlador Thesis.
                    configuration.For<ThesisController>(x => x.Index()).RequireAnyRole(rolesThesisIndex);

                    //configuration.For<ThesisController>(x => x.Create()).RequireAnyRole(rolesThesisCreate);

                    configuration.For<ThesisController>(x => x.Edit(default(Guid))).RequireAnyRole(rolesThesisEdit);

                    //configuration.For<ThesisController>(x => x.Delete(default(Guid))).RequireAnyRole(rolesThesisDelete);

                    //configuration.For<ThesisController>(x => x.DeleteConfirmed(default(Guid))).RequireAnyRole(rolesThesisDeleteConfirmed);

                    configuration.For<ThesisController>(x => x.Details(default(Guid))).RequireAnyRole(rolesThesisDetails);

                    #endregion

                    #region Reglas de reporte
                    string[] rolesReportsIndex = assembly.GetRolesMethods("ReportsController", "Index");
                    string[] rolesReportsDetails = assembly.GetRolesMethods("ReportsController", "Details");
                    string[] rolesReportsCreate = assembly.GetRolesMethods("ReportsController", "Create");
                    string[] rolesReportsEdit = assembly.GetRolesMethods("ReportsController", "Edit");
                    string[] rolesReportsDelete = assembly.GetRolesMethods("ReportsController", "Delete");
                    string[] rolesReportsDeleteConfirmed = assembly.GetRolesMethods("ReportsController", "DeleteConfirmed");
                    string[] rolesReportsPreview = assembly.GetRolesMethods("ReportsController", "Preview");
                    string[] rolesReportsRenderReport = assembly.GetRolesMethods("ReportsController", "RenderReport");



                    //Se configuran las reglas de acceso para el controlador surveys.
                    configuration.For<ReportsController>(x => x.Index())
                                 .RequireAnyRole(rolesReportsIndex);
                    configuration.For<ReportsController>(x => x.Create())
                                .RequireAnyRole(rolesReportsCreate);
                    configuration.For<ReportsController>(x => x.Details(default(Guid)))
                                .RequireAnyRole(rolesReportsDetails);
                    configuration.For<ReportsController>(x => x.Edit(default(Guid)))
                                .RequireAnyRole(rolesReportsEdit);
                    configuration.For<ReportsController>(x => x.Delete(default(Guid)))
                                .RequireAnyRole(rolesReportsDelete);
                    configuration.For<ReportsController>(x => x.DeleteConfirmed(default(Guid)))
                                .RequireAnyRole(rolesReportsDeleteConfirmed);
                    configuration.For<ReportsController>(x => x.Preview(default(Guid)))
                                .RequireAnyRole(rolesReportsPreview);
                    configuration.For<ReportsController>(x => x.RenderReport(default(Guid)))
                                .RequireAnyRole(rolesReportsRenderReport);
                    #endregion

                    #region Reglas de seguridad del controlador SurveysTopic

                    string[] rolesSurveysTopicsIndex = assembly.GetRolesMethods("SurveysTopicsController", "Index");
                    string[] rolesSurveysTopicsCreate = assembly.GetRolesMethods("SurveysTopicsController", "Create");
                    string[] rolesSurveysTopicsEdit = assembly.GetRolesMethods("SurveysTopicsController", "Edit");
                    string[] rolesSurveysTopicsDelete = assembly.GetRolesMethods("SurveysTopicsController", "Delete");
                    string[] rolesSurveysTopicsDeleteConfirmed = assembly.GetRolesMethods("SurveysTopicsController", "DeleteConfirmed");

                    configuration.For<SurveysTopicsController>(x => x.Index(default(Guid)))
                                 .RequireAnyRole(rolesSurveysTopicsIndex);
                    configuration.For<SurveysTopicsController>(x => x.Create(default(Guid)))
                                .RequireAnyRole(rolesSurveysTopicsCreate);
                    configuration.For<SurveysTopicsController>(x => x.Edit(default(Guid), default(Guid)))
                                .RequireAnyRole(rolesSurveysTopicsEdit);
                    configuration.For<SurveysTopicsController>(x => x.Delete(default(Guid), default(Guid)))
                                .RequireAnyRole(rolesSurveysTopicsDelete);
                    configuration.For<SurveysTopicsController>(x => x.DeleteConfirmed(default(Guid), default(Guid)))
                                .RequireAnyRole(rolesSurveysTopicsDeleteConfirmed);
                    

                    #endregion

                    #region Reglas de seguridad del Controlador RoleMethods
                    string[] rolesRoleMethodsAssignRolesMethods = assembly.GetRolesMethods("RoleMethodsController", "AssignRolesMethods");
                    configuration.For<RoleMethodsController>(x => x.AssignRolesMethods(default(Guid)))
                                 .RequireAnyRole(rolesSurveysTopicsIndex);

                    #endregion

                    #region Reglas de seguridad del controlador
                    string[] rolesUsersRolesControllerAssignUserRoles = assembly.GetRolesMethods("UsersRolesController", "AssignUserRoles");
                    configuration.For<UsersRolesController>(x => x.AssignUserRoles(default(Guid)))
                                 .RequireAnyRole(rolesUsersRolesControllerAssignUserRoles);

                    #endregion

                    #endregion
                    //****************************************************************************************************//
                    //****************************************************************************************************//
                    //****************************************************************************************************//
                



            });
            #endregion
                //Se añaden las reglas
                GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);
    //            //GlobalFilters.Filters.Add(new DenyAnonymousAccessPolicyViolationHandler());
            }
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //CreateSteps();

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
    }

}