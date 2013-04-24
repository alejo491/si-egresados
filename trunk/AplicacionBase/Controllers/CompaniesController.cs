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
    public class CompaniesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Companies/

        public ViewResult Index()
        {
            return View(db.Companies.ToList());
        }

        //
        // GET: /Companies/Details/5

        public ViewResult Details(Guid id)
        {
            Company company = db.Companies.Find(id);
            return View(company);
        }

        //
        // GET: /Companies/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Companies/Create

        [HttpPost]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                company.Id = Guid.NewGuid();
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(company);
        }
        
        //
        // GET: /Companies/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Company company = db.Companies.Find(id);
            return View(company);
        }

        //
        // POST: /Companies/Edit/5

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        //
        // GET: /Companies/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Company company = db.Companies.Find(id);
            return View(company);
        }

        //
        // POST: /Companies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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