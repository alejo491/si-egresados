using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    [Authorize]
    public class VacanciesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Vacancies/

        public ViewResult Index()
        {
            var vacancies = db.Vacancies.Include(v => v.Company).Include(v => v.User);
            return View(vacancies.ToList());
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
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber");
            return View();
        } 

        //
        // POST: /Vacancies/Create

        [HttpPost]
        public ActionResult Create(Vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                vacancy.Id = Guid.NewGuid();
                db.Vacancies.Add(vacancy);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", vacancy.IdCompanie);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);
            return View(vacancy);
        }
        
        //
        // GET: /Vacancies/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Vacancy vacancy = db.Vacancies.Find(id);
            ViewBag.IdCompanie = new SelectList(db.Companies, "Id", "Name", vacancy.IdCompanie);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);
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
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", vacancy.IdUser);
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
    }
}