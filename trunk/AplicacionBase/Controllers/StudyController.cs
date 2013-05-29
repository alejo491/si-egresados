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
    public class StudyController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Study/

        public ViewResult Index()
        {
            var studies = db.Studies.Include(s => s.School).Include(s => s.User).Include(s => s.Thesis);
            return View(studies.ToList());
        }

        //
        // GET: /Study/Details/5

        public ViewResult Details(Guid id)
        {
            Study study = db.Studies.Find(id);
            return View(study);
        }

        //
        // GET: /Study/Create

        public ActionResult Create()
        {
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name");
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber");
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title");
            return View();
        } 

        //
        // POST: /Study/Create

        [HttpPost]
        public ActionResult Create(FormCollection form) 
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {

                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }

            }
            var IdUser = g;
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
                    }
                }
                if(key.Contains("Programas")) 
                {
                    
                }
                if (key.Contains("Grade")) 
                {
                    study.Grade = form[key].ToString();
                }
                if (key.Contains("Elective1")|| key.Contains("Elective2")|| key.Contains("Elective3")|| key.Contains("Elective4")|| key.Contains("Elective5"))
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
                if (key.Contains("txtTesis"))
                {
                    Tesis.IdStudies = study.Id;
                    Tesis.Title = form[key].ToString();
                }
                if (key.Contains("txtDescripcion"))
                {
                    if (form[key].Length == 0) { study.Thesis = Tesis;}
                    Tesis.Description = form[key].ToString();
                    study.Thesis = Tesis;
                }

            }
            db.Studies.Add(study);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Create(Study study)
        //{
        //    Guid g = System.Guid.Empty;
        //    foreach (var e in db.aspnet_Users)
        //    {

        //        if (e.UserName == HttpContext.User.Identity.Name)
        //        {
        //            g = e.UserId;
        //        }

        //    }
        //    var IdUser = g;
        //    if (ModelState.IsValid)
        //    {
        //        ViewBag.IdUser = IdUser;
        //        study.Id = Guid.NewGuid();
        //        study.IdUser = IdUser;
        //        db.Studies.Add(study);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");  
        //    }
            
        //    ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name", study.IdSchool);
        //    //ViewBag.IdUser = new SelectList(db.Users, "Id", "Name", study.IdUser);
        //    ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title", study.Id);
        //    return View(study);
        //}
        
        //
        // GET: /Study/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Study study = db.Studies.Find(id);
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name", study.IdSchool);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", study.IdUser);
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title", study.Id);
            return View(study);
        }

        //
        // POST: /Study/Edit/5

        [HttpPost]
        public ActionResult Edit(Study study)
        {
            if (ModelState.IsValid)
            {
                db.Entry(study).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name", study.IdSchool);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", study.IdUser);
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title", study.Id);
            return View(study);
        }

        //
        // GET: /Study/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Study study = db.Studies.Find(id);
            return View(study);
        }

        //
        // POST: /Study/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Study study = db.Studies.Find(id);
            db.Studies.Remove(study);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AutocompleteSchool(string term)
        {
            var items = (from u in db.Schools select u.Name).ToArray();
            var filteredItems = items.Where(
                item => item.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocompleteElective(string term)
        {
            var items = (from u in db.Electives select u.Name).ToArray();
            var filteredItems = items.Where(
                item => item.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocompleteThesis(string term)
        {
            var items = (from u in db.Theses select u.Title).ToArray();
            var filteredItems = items.Where(
                item => item.StartsWith(term, StringComparison.InvariantCultureIgnoreCase));
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}