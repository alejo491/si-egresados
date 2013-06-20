using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
using PagedList;


namespace AplicacionBase.Controllers
{
    [Authorize]
    public class VacanciesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        private int pageSize = 10;
        private int pageNumber;

        //! Renderiza la pagina principal de vacantes
        /*!
         * \return La vista con el listado de vacantes sacadas de la base de datos
         *
         */
        public ViewResult Index(int? page)
        {
            var vacancies = db.Vacancies.Include(v => v.Company).Include(v => v.User);
            pageNumber = (page ?? 1);
            return View(vacancies.ToList().OrderByDescending(v => v.PublicationDate).ToPagedList(pageNumber, pageSize));
            // return View(vacancies.ToList().OrderByDescending(v => v.PublicationDate));
        }

        public ActionResult Index2()
        {
            var vacancies = db.Vacancies.Include(v => v.Company).Include(v => v.User).OrderByDescending(v => v.PublicationDate).Take(5);
            return PartialView(vacancies.ToList());
        }
        //! Muestra los detalles para una vacante en especial
        /*!
         * \param id Contiene el id de la vacante de la cual se desean los detalles
         * \return La vista con la vacante en detalle
         *
         */
        public ViewResult Details(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            return View(vacancy);
        }

        //! Atiende el resultado de pulsar el boton de Crear Nueva vacante en la vista principal
        /*!
         * \return La vista de Creacion solamente en el caso de que el usuario este loggeado
         *
         */
        public ActionResult Create()
        {
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name");
            ViewBag.IdUser = new SelectList(db.Users, "Id", "Id");
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {
                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }
            }
            var IdUser = g;

            bool dataUpdate = false;
            foreach (var User in db.Users) // Se busca si el usuario ha actualizado los datos de la cuenta
            {
                if (User.Id == IdUser)
                {
                    dataUpdate = true;
                }

            }

            if (dataUpdate)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ErrorDataUpdate", "Error");
            }
        }

        //! Atiende el resultado de hacer clic en el boton de Crear Nueva desde la vista de Creacion de Vacantes
        /*!
         * \param vacancy Contiene los datos de la vacantes para ser llevados a la base de datos
         * \return La vista al listado de vacantes
         *
         */

        [HttpPost]
        public ActionResult Create(Vacancy vacancy)
        {

            /* Guid g = System.Guid.Empty;
             foreach (var e in db.aspnet_Users)
             {

                 if (e.UserName == HttpContext.User.Identity.Name)
                 {
                     g = e.UserId;
                 }

             }
             var IdUser = g;

             */




            var IdUser = db.aspnet_Users.Where(u => u.UserName.Equals(HttpContext.User.Identity.Name)).First().UserId;



            if (ModelState.IsValid)
            {
                vacancy.Id = Guid.NewGuid();
                vacancy.IdUser = IdUser;
                vacancy.PublicationDate = DateTime.Now;
                db.Vacancies.Add(vacancy);
                db.SaveChanges();
                TempData["Create"] = "Se ha ingresado correctamente la vacante !";
                return RedirectToAction("Index");
            }

            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", vacancy.IdCompanie);
            //ViewBag.IdUser = new SelectList(db.Users, "Id", "Id", vacancy.IdUser);

            return View(vacancy);
        }

        //! Atiende el resultado de hacer clic en Editar, en las opciones de cada vacante
        /*!
         * \param id Contiene el id de la vacante que se desea modificar
         * \return La vista con los datos a editar de la vacante
         *
         */
        public ActionResult Edit(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", vacancy.IdCompanie);
            // ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);
            return View(vacancy);
        }

        //! Atiende el resultado de hacer clic en Editar de la vista de Edicion de vacantes
        /*!
         * \param vacancy Contiene los datos de la vacante a actualizar
         * \return La vista con el listado de vacantes
         *
         */
        [HttpPost]
        public ActionResult Edit(Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacancy).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Update"] = "Se ha actualizado correctamente la información de la vacante !";
                return RedirectToAction("Index");
            }
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", vacancy.IdCompanie);
            //   ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);

            return View(vacancy);
        }

        //! Atiende el resultado de hacer clic en Eliminar, en las opciones de cada vacante
        /*!
         * \param id Contiene el id de la vacante que se desea Eliminar
         * \return La vista de confirmación
         *
         */
        public ActionResult Delete(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            return View(vacancy);
        }

        //! Atiende el resultado de hacer clic en Eliminar de la vista de Confirmacion de eliminacion de vacantes
        /*!
         * \param id Id de la vacante que se confirma se desea eliminar
         * \return La vista con el listado de vacantes
         *
         */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            db.Vacancies.Remove(vacancy);
            db.SaveChanges();
            TempData["Delete"] = "Se ha borrado la vacante seleccionada!";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        //private System.Linq.IOrderedEnumerable<Vacancy> results;
        //private string searchText;

        //! Atiende el resultado de hacer clic en Buscar de la vista Principal
        /*!
         * \param criteria Contiene las palabras clave con las que se desea hacer la busqueda
         * \param page Elemento de control para la paginación
         * \return La vista con el listado de vacantes encontradas para las palabras claves
         *
         */
        public ActionResult Search(string criteria, int? page)
        {

            ViewBag.CurrentFilter = criteria;

            if (criteria == null)
            {
                criteria = "";
            }



            /*    if (criteria.ToLower().Trim().Equals(searchText))
                {
                    pageNumber = (page ?? 1);
                    return View(results.ToPagedList(pageNumber, pageSize));
                }
            */



            string searchText = criteria.ToLower().Trim();


            //Búsqueda
            var vacancies = db.Vacancies.Where(v => v.Charge.ToLower().Contains(criteria) || v.Description.Contains(criteria) ||
                v.ProfessionalProfile.Contains(criteria));

            //Ordenar por fecha de publicación
            var results = vacancies.ToList().OrderByDescending(c => c.PublicationDate);


            pageNumber = (page ?? 1);
            return View(results.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult AutocompleteCompanies(string term)
        {
            var items = (from u in db.Companies select u.Name).ToArray();
            var filteredItems = items.Where(
                item => item.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }


    }
}