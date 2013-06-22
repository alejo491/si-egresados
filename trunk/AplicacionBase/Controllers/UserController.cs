using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AplicacionBase.Models;
using PagedList;

namespace AplicacionBase.Controllers
{
    /// <summary>
    /// Controlador para gestionar los usuarios que estan registrados en el sistema
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// Atributo que consulta la base de datos.
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();
        /// <summary>
        /// Atributos que permite controlar la paginación de las vistas.
        /// </summary>
        private int pageSize = 6;
        private int pageNumber;
        /// <summary>
        /// Atributo que recorre la lista de usuarios.
        /// </summary>
        private System.Linq.IOrderedEnumerable<User> results;

        #region Página Principal Usuarios
        //
        // GET: /User/

        /// <summary>
        /// Método que carga la vista que contiene todos los usuarios registrados en el sistema
        /// </summary>
        /// <returns>Vista que contine todos los usuarios</returns>
        /// <returns>Redirecciona al perfil personal del usuario</returns>
        public ActionResult Index(int? page)
        {
            Guid g = searchId();
            var list = Roles.GetRolesForUser();
            var temp = false;
            foreach (var i in list) { if (i == "Administrador") { temp = true;}}
            if (temp)
            {
                pageNumber = (page ?? 1);
                var users = db.Users.Include(u => u.aspnet_Users);
                ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", users.ToList());
                return View(users.ToList().ToPagedList(pageNumber, pageSize));
            }
            else { return RedirectToAction("Begin", "User", new { id = g }); }
        }
        #endregion

        #region Perfil Personal del Usuario
        /// <summary>
        /// Método que carga la vista principal de un usuario
        /// </summary>
        /// <param name="id">Id del usuario que ingresa al sistema</param>
        /// <returns>Vista principal de un usuario con todas sus opciones</returns>
        public ActionResult Begin(Guid id)
        {
            User user = db.Users.Find(id);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }
        #endregion

        #region Detalles de Usuario
        //
        // GET: /User/Details/5
        /// <summary>
        /// Método que carga la vista que contine los datos de un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>Vista para consultar los datos de un usuario</returns>
        public ViewResult Details(Guid id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }
        #endregion

        #region Crear Usuarios
        //
        // GET: /User/Create
        /// <summary>
        /// Método que carga la vista con el formulario para crear la información personal de un usuario
        /// </summary>
        /// <returns>Vista que despliega el formulario que permite crear los datos</returns>
        public ActionResult Create()
        {
            ViewBag.UserName = HttpContext.User.Identity.Name;
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName");
            return View();
        }
        #endregion

        #region Obtener Id del usuario activo
        /// <summary>
        /// Método que obtiene el Id del usuario que esta logueado en el sistema
        /// </summary>
        /// <returns>Id del usuario</returns>
        public Guid searchId()
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {
                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }
            }
            return g;
        }
        #endregion

        #region Almacenar Usuarios
        //
        // POST: /User/Create
        /// <summary>
        /// Guarda los datos del usuario recibidos en el formulario
        /// </summary>
        /// <param name="user">Usuario con toda su información</param>
        /// <returns>Redirecciona al perfil de Usuario</returns>
        /// <returns>Redirecciona al index de Wizard</returns>
        /// <returns>Vista que despliega el formulario que permite crear los datos </returns>
        [HttpPost]
        public ActionResult Create(User user)
        {
            Guid g = searchId();
            if (ModelState.IsValid && !g.Equals(System.Guid.Empty))
            {
                user.Id = g;
                user.States = "true";
                db.Users.Add(user);
                db.SaveChanges();
                CreateSteps(g);
                StepsLoad(g);
                var steps = db.UsersSteps.Where(s => s.UserId == g).OrderBy(s => s.Step.SOrder);
                if (!steps.Any())
                {
                    return RedirectToAction("Begin", "User", new { id = g });
                }
                else
                {
                    var tmp = (List<UsersStep>)steps.ToList();
                    Session["steps"] = tmp;
                    var ActualStep = Convert.ToInt16(tmp.ElementAt(0).Step.SOrder);
                    return RedirectToAction("Index", "Wizard", ActualStep);
                }
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }
        #endregion

        #region Administrar Wizard
        /*Lineas Necesarias, para Administrar el Wizard*/
        /// <summary>
        /// Se cargan los pasos que son necesarios que haga un
        /// usuario determinado
        /// </summary>
        /// <param name="user">Identificador único del usuario </param>
        private void StepsLoad(Guid user)
        {
            var steps = db.Steps.OrderBy(s => s.SOrder).ToList();
            foreach (var step in steps)
            //while (count < steps.Count())
            {
                //var step = steps.ElementAt(count);
                var obj = new UsersStep
                {
                    UserId = user,
                    IdSteps = step.Id,
                    Ok = "f"
                };
                db.UsersSteps.Add(obj);
                db.SaveChanges();
            }
        }
        #endregion

        #region Pasos del Wizard

        /*Lineas Necesarias, para Administrar el Wizard*/
        /// <summary>
        /// Se Crean los pasos del Wizard
        /// </summary>
        private void CreateSteps(Guid id)
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

                        obj.SPath = @"/Study/Index?id=" + id.ToString() + @"&wizardStep=1";
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
        #endregion

        #region Editar Usuario
        //
        // GET: /User/Edit/5
        /// <summary>
        /// Método que carga la vista con el formulario para editar la información personal de un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>Vista que despliega el formulario con los datos para editarlos</returns>
        public ActionResult Edit(Guid id)
        {
            User user = db.Users.Find(id);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }
        #endregion

        #region Editar Usuario (Almacenar Cambios)
        //
        // POST: /User/Edit/5
        /// <summary>
        /// Guarda los cambios de la información del usuario recibidos en el formulario
        /// </summary>
        /// <param name="user">Usuario con toda su información</param>
        /// <returns> Redirecciona al perfil personal del usuario</returns>
        /// <returns> Redirecciona al index del usuario</returns>
        /// <returns> Redirecciona a la vista principal del usuario con todas sus funcionalidades</returns>
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                String Name = HttpContext.User.Identity.Name;
                Guid g = searchId();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                if (Name == user.aspnet_Users.UserName)
                {
                    return RedirectToAction("Begin", "User", new { id = g });
                }
                else
                {
                    TempData["Editar"] = "¡ Datos Del Usuario Modificados Correctamente !";
                    return RedirectToAction("Index", "User");
                }
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }
        #endregion

        #region Cambiar estado
        /// <summary>
        /// Método que carga la vista que permite modificar el estado de un usuario en el sistema
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>Vista que da la opción de cambiar de estado a un usuario</returns>
        public ActionResult State(Guid id)
        {
            User user = db.Users.Find(id);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }
        #endregion

        #region Cambiar Estado (Almacenar Cambios)
        /// <summary>
        /// Guarda los cambios realizados al estado de un usuario
        /// </summary>
        /// <param name="user">Usuario con toda su información</param>
        /// <returns> Redirecciona al index del usuario</returns>
        ///  <returns> Redirecciona a la vista del usuario administrador</returns>

        [HttpPost]
        public ActionResult State(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Estado"] = "¡ Estado del Usuario Modificado Correctamente !";
                return RedirectToAction("Index", "User");
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }
        #endregion

        #region Registro de usuario
        /// <summary>
        /// Método que carga la vista del formulario para registrar un usuario en el sistema
        /// </summary>
        /// <returns>Vista que despliega el formulario de registro</returns>
        public ActionResult Register()
        {
            return View();
        }
        #endregion

        #region Registrar usuarios (Almacenar cambios)
        /// <summary>
        /// Guarda el usuario creado en el sistema
        /// </summary>
        /// <param name="model">Usuario a registrar</param>
        /// <returns>Redirecciona al user generate para almacenar la información correspondiente al usuario</returns>
        /// <returns>Redirecciona a la vista de registro de datos </returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Guid g = System.Guid.Empty;
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);
                foreach (var e in db.aspnet_Users)
                {
                    if (e.UserName == model.UserName)
                    {
                        g = e.UserId;
                    }
                }
                TempData["Registrado"] = "¡ El Usuario Registrado Correctamente !";
                TempData["info"] = "Si desea complete la Información del Usuario";
                return RedirectToAction("Generate", "User", new { id = g });
            }
            return View(model);
        }
        #endregion

        #region Crear Información del Usuario (Funcion Administrador) 
        /// <summary>
        /// Método que carga la vista con el formulario para crear la información personal de un usuario por parte del administrador
        /// </summary>
        /// <returns>Vista que despliega el formulario que permite crear los datos</returns>
        public ActionResult Generate(Guid id)
        {
            User user = db.Users.Find(id);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName");
            return View(user);
        }
        #endregion

        #region Crear Información del Usuario (Almacenar cambios)
        /// <summary>
        /// Guarda los datos del usuario creado por parte del administrador
        /// </summary>
        /// <param name="user">Usuario con toda su información</param>
        /// <returns>Redirecciona al index del usuario</returns>
        /// <returns>Muestra los usuario registrados</returns>
        [HttpPost]
        public ActionResult Generate(User user)
        {
            if (ModelState.IsValid)
            {
                user.States = "true";
                db.Users.Add(user);
                db.SaveChanges();
                TempData["Creado"] = "¡ El Usuario se Creó Correctamente !";
                return RedirectToAction("Index", "User");
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }
        #endregion

        #region salir
        /// <summary>
        /// Método que permite salir de la vista principal de un usuario
        /// </summary>
        /// <param name="id">Id del usuario que ingresa al sistema</param>
        /// <returns>Redireccionar al index de usuario</returns>
        /// <returns>Redireccionar a la pagina inicial (Home)</returns>
        public ActionResult Out(Guid id)
        {
            Guid g = System.Guid.Empty;
            User user = db.Users.Find(id);
            VerifyController v = new VerifyController();
            if (user.aspnet_Users.UserName != HttpContext.User.Identity.Name)
            {
                g = searchId();
            }
            else
            {
                g = user.Id;
            }
            bool salir = v.Begin(g);
            if (salir)
            {
                return RedirectToAction("Index", "User"); ;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region Buscador
        /// <summary>
        /// Método que permite buscar usuarios
        /// </summary>
        /// <param name="nameuser">Parametro de búsqueda</param>
        /// <param name="page">Número de la página</param>
        /// <returns>Redirecciona a la vista del buscador </returns>
        public ActionResult Search(string nameuser, int? page)
        {
            ViewBag.CurrentFilter = nameuser;
            if (nameuser == null)
            {
                nameuser = " ";
            }
            var users = db.Users.Where(u => u.FirstNames.ToLower().Contains(nameuser) || u.LastNames.Contains(nameuser));
            results = users.ToList().OrderByDescending(c => c.CellphoneNumber);
            ViewBag.Busqueda = results;
            pageNumber = (page ?? 1);
            return View(results.ToPagedList(pageNumber, pageSize));
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
