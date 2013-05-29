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
        private int pageSize = 1;
        private int pageNumber;

        //
        // GET: /Vacancies/

        public ViewResult Index()
        {
            var vacancies = db.Vacancies.Include(v => v.Company).Include(v => v.User);
            return View(vacancies.ToList().OrderByDescending(v => v.PublicationDate));
        }

        //
        // GET: /Vacancies/Details/5

        public ViewResult Details(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            return View(vacancy);
        }

        //
        // GET: /Vacancies/Create

        public ActionResult Create()
        {
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name");
            ViewBag.IdUser = new SelectList(db.Users, "Id", "Id");
            return View();
        }

        //
        // POST: /Vacancies/Create

        [HttpPost]
        public ActionResult Create(Vacancy vacancy)
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

        //
        // GET: /Vacancies/Edit/5

        public ActionResult Edit(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", vacancy.IdCompanie);
            // ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);
            return View(vacancy);
        }

        //
        // POST: /Vacancies/Edit/5

        [HttpPost]
        public ActionResult Edit(Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacancy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", vacancy.IdCompanie);
            //   ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);
            return View(vacancy);
        }

        //
        // GET: /Vacancies/Delete/5

        public ActionResult Delete(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            return View(vacancy);
        }

        //
        // POST: /Vacancies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            db.Vacancies.Remove(vacancy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing); 
        }


       
        private System.Linq.IOrderedEnumerable<Vacancy> results;
        private string searchText;
   
        
        public ActionResult Search(string criteria, int? page)
        {
       
            ViewBag.CurrentFilter = criteria;
        
            if (criteria == null) {
               criteria = "";
            }


          
            /*    if (criteria.ToLower().Trim().Equals(searchText))
                {
                    pageNumber = (page ?? 1);
                    return View(results.ToPagedList(pageNumber, pageSize));
                }
            */

           

           searchText= criteria.ToLower().Trim();

           
            //Búsqueda
            var vacancies= db.Vacancies.Where(v => v.Charge.ToLower().Contains(criteria) || v.Description.Contains(criteria) ||
                v.ProfessionalProfile.Contains(criteria));

            //Ordenar por fecha de publicación
            results = vacancies.ToList().OrderByDescending(c => c.PublicationDate);



            pageNumber = (page ?? 1);            
            return View(results.ToPagedList(pageNumber, pageSize));

        }

     
      
       
    }
}