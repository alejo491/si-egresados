using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
using System.Web.Security;
using System.Web.Routing;


namespace AplicacionBase.Controllers
{
    /// <summary>
    /// Controlador para gestionar los estudios que estan registrados en el sistema
    /// </summary>
    public class StudyController : Controller
    {
        /// <summary>
        /// Atributo que consulta la base de datos.
        /// </summary>
        private DbSIEPISContext db = new DbSIEPISContext();
        /// <summary>
        /// Atributos que permite controlar la paginación de las vistas.
        /// </summary>
       
        //
        // GET: /Study/

        /// <summary>
        /// Método que carga la vista que contiene todos los estudios registrados en el sistema de un usuario en particular
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="page">Paginación</param>
        /// <returns>Vista que contine los datos de los estudios de un usuario</returns>
        #region Index(id page)
        public ActionResult Index(Guid id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
               
                ViewBag.UserId = id;
                var studies = db.Studies.Include(s => s.School).Include(s => s.User).Include(s => s.Thesis).Where(s => s.IdUser == id);
                return View(studies.ToList());
            }
            else
            {
                return RedirectToAction("Begin", "User", new RouteValueDictionary(new { controller = "User", action = "Begin", Id = id }));
            }
        }
        #endregion 

        //
        // GET: /Study/Details/5
        /// <summary>
        /// Método que carga la vista que contine los datos del estudio de un usuario
        /// </summary>
        /// <param name="id">Id del estudio</param>
        /// <returns>Vista para consultar los datos de un estudio</returns>
        #region details(id)
        public ViewResult Details(Guid id, Guid idUser)
        {
            ViewBag.UserId = idUser;
            Study study = db.Studies.Find(id);
            return View(study);
        }
        #endregion

        //
        // GET: /Study/Create
        /// <summary>
        /// Método que carga la vista con el formulario para crear la información del estudio de un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <returns>Vista que despliega el formulario que permite crear los datos</returns>
        #region Create(id)
        public ActionResult Create(Guid id)
        {
            ViewBag.UserId = id;
            User user = db.Users.Find(id);
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name");
            ViewBag.IdUser = new SelectList(db.Users, "Id", "Name");
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title");
            return View(user);
        }
        #endregion

        //
        // POST: /Study/Create
        /// <summary>
        /// Guarda los datos del estudio recibidos en el formulario, ademas de los datos adicionales en las demas tablas asociadas al estudio
        /// </summary>
        /// <param name="form">formulario con toda la información</param>
        /// <param name="id">Id del usuario</param>
        /// <returns></returns>
        [HttpPost]
        #region create(form, id)
        public ActionResult Create(FormCollection form, Guid id)
        {
            ViewBag.UserId = id;
            var IdUser = id;
            Study study = new Study();
            Thesis Tesis = new Thesis();
            study.IdUser = IdUser;
            study.Id = Guid.NewGuid();

            foreach (String key in form)
            {
                if (key.Contains("txtSchool"))
                {
                    string var = form[key].ToString();
                    bool temp = false;
                    foreach (School school in db.Schools)
                    {
                        if (school.Name == var)
                        {
                            study.IdSchool = school.Id;
                            temp = true;
                        }
                    }
                    if (temp == false)
                    {
                        School school = new School();
                        school.Id = Guid.NewGuid();
                        school.Name = var;
                        db.Schools.Add(school);
                        db.SaveChanges();
                        study.IdSchool = school.Id;
                    }
                }
                if (key.Contains("programas"))
                {
                    study.Programs = form[key];
                }
                if (key.Contains("Grade"))
                {
                    study.Grade = form[key].ToString();
                }
                if (key.Contains("Elective1") || key.Contains("Elective2") || key.Contains("Elective3") || key.Contains("Elective4") || key.Contains("Elective5"))
                {
                    if (form[key].Length != 0)
                    {
                        string var = form[key].ToString();
                        bool temp = false;
                        foreach (Elective elective in db.Electives)
                        {
                            if (elective.Name == var)
                            {
                                study.Electives.Add(elective);
                                temp = true;
                            }
                        }
                        if (temp == false)
                        {
                            Elective elective = new Elective();
                            elective.Id = Guid.NewGuid();
                            elective.Name = var;
                            db.Electives.Add(elective);
                            db.SaveChanges();
                            study.Electives.Add(elective);
                        }
                    }
                }
                if (key.Contains("txtStartDate"))
                {
                    DateTime var = DateTime.Parse(form[key].ToString());
                    study.StartDate = var;
                }
                if (key.Contains("txtEndDate"))
                {
                    DateTime var = DateTime.Parse(form[key].ToString());
                    study.EndDate = var;
                }
                if (key.Contains("txtTesis") && form[key].Length != 0)
                {
                    Tesis.IdStudies = study.Id;
                    Tesis.Title = form[key].ToString();
                    study.Thesis = Tesis;
                }
                if (key.Contains("txtDescripcion") && study.Thesis != null)
                {
                    if (form[key].Length == 0)
                    {
                        study.Thesis = Tesis;
                    }
                    else
                    {
                        Tesis.Description = form[key].ToString();
                        study.Thesis = Tesis;
                    }
                }

            }
            db.Studies.Add(study);
            db.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Study", action = "Index", Id = id }));
        }
        #endregion

        //
        // GET: /Study/Edit/5
        /// <summary>
        /// Método que carga la vista con el formulario para editar la información de un estudio
        /// </summary>
        /// <param name="id">Id del estudio</param>
        /// <returns>Vista que despliega el formulario con los datos para editarlos</returns>
        #region Edit(id)
        public ActionResult Edit(Guid id, Guid idUser)
        {
            ViewBag.UserId = idUser;
            Study study = db.Studies.Find(id);
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name", study.IdSchool);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", study.IdUser);
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title", study.Id);
            var electives = study.Electives;
            for (int i = electives.Count; i < 5; i++)
            {
                electives.Add(new Elective());
            }

            return View(study);
        }
        #endregion
        
        //
        // POST: /Study/Edit/5
        /// <summary>
        /// Guarda los cambios de la información del estudio recibido en el formulario, asi como de las tablas relacionadas
        /// </summary>
        /// <param name="id">Id del estudio</param>
        /// <param name="form">formulario con la información digitada</param>
        /// <param name="study">Estudio modificado</param>
        /// <returns></returns>
        [HttpPost]
        #region Edit(id, study, form)
        public ActionResult Edit(Guid id, Study study, FormCollection form)
        {
            ViewBag.userId = id;
            var last = db.Studies.Find(id);
            var s = study.School.Name;
            var flag = false;
            var electives = study.Electives;
            foreach (var i in db.Schools)
            {
                if (i.Name == study.School.Name)
                {
                    last.IdSchool = i.Id;
                    flag = true;
                }
            }
            if (flag == false)
            {
                var newid = Guid.NewGuid();
                db.Schools.Add(new School { Id = newid, Name = s });
                db.SaveChanges();
                last.IdSchool = newid;
            }

            last.Grade = study.Grade;

            last.Electives.Clear();
            foreach (String key in form)
            {
                if (key.Contains("programas"))
                {
                    last.Programs = form[key];
                }
                if (key.Contains("Elective1") || key.Contains("Elective2") || key.Contains("Elective3") || key.Contains("Elective4") || key.Contains("Elective5"))
                {
                    if (form[key].Length != 0)
                    {
                        string var = form[key].ToString();
                        bool temp = false;
                        foreach (Elective elective in db.Electives)
                        {
                            if (elective.Name == var)
                            {
                                bool el2 = false;
                                foreach (var i in last.Electives)
                                {
                                    if (i.Id == elective.Id) { el2 = true; }
                                }
                                if (el2 == false)
                                {
                                    last.Electives.Add(elective);
                                }
                                temp = true;
                            }
                        }
                        if (temp == false)
                        {
                            Elective elective = new Elective();
                            elective.Id = Guid.NewGuid();
                            elective.Name = var;
                            db.Electives.Add(elective);
                            last.Electives.Add(elective);
                        }
                    }
                }
            }

            last.StartDate = study.StartDate;
            last.EndDate = study.EndDate;

            Thesis tesis = new Thesis();
            if (study.Thesis.Title != null)
            {
                tesis.IdStudies = last.Id;
                tesis.Title = study.Thesis.Title;
                tesis.Description = study.Thesis.Description;
                if (last.Thesis != null) { db.Theses.Remove(last.Thesis); }
                db.Theses.Add(tesis);
                last.Thesis = tesis;
            }

            if (ModelState.IsValid)
            {
                db.Entry(last).State = EntityState.Modified;
                db.SaveChanges();

            }
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Study", action = "Index", Id = last.IdUser }));
        }
        #endregion

        //
        // GET: /Study/Delete/5
        /// <summary>
        /// Método que carga la vista con el formulario para eliminar un estudio
        /// </summary>
        /// <param name="id">Id del estudio</param>
        /// <returns>Vista que despliega el formulario con los datos para eliminar</returns>
        #region Delete(id, idUser)
        public ActionResult Delete(Guid id, Guid idUser)
        {
            ViewBag.IdUser = idUser;
            Study study = db.Studies.Find(id);
            return View(study);
        }
        #endregion

        //
        // POST: /Study/Delete/5
        /// <summary>
        /// Elimina el estudio solicitado por el usuario
        /// </summary>
        /// <param name="id">Id del estudio</param>
        /// <param name="form">formulario con la información digitada</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        #region Delete(id, idUser)
        public ActionResult DeleteConfirmed(Guid id, Guid idUser)
        {
            ViewBag.UserId = idUser;
            Study study = db.Studies.Find(id);
            db.Studies.Remove(study);
            db.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Study", action = "Index", Id = idUser }));
        }
        #endregion

        /// <summary>
        /// Permite hacer autocompletar en las vistas, consultando en la tabla de schools de la base de datos
        /// </summary>
        /// <param name="term">Termino digitado</param>
        /// <returns>resultados de la consulta</returns>
        #region AutocompleteSchool(term)
        public ActionResult AutocompleteSchool(string term)
        {
            var items = (from u in db.Schools select u.Name).ToArray();
            var filteredItems = items.Where(
                item => item.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// Permite hacer autocompletar en las vistas, consultando en la tabla de Electivas de la base de datos
        /// </summary>
        /// <param name="term">Termino digitado</param>
        /// <returns>resultados de la consulta</returns>
        #region AutocompleteElective(term)
        public ActionResult AutocompleteElective(string term)
        {
            var items = (from u in db.Electives select u.Name).ToArray();
            var filteredItems = items.Where(
                item => item.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// Permite hacer autocompletar en las vistas, consultando en la tabla de Tesis de la base de datos
        /// </summary>
        /// <param name="term">Termino digitado</param>
        /// <returns>resultados de la consulta</returns>
        #region AutocompleteThesis(term)
        public ActionResult AutocompleteThesis(string term)
        {
            var items = (from u in db.Theses select u.Title).ToArray();
            var filteredItems = items.Where(
                item => item.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}