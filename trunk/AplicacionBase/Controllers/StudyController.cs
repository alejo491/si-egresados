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

        public ActionResult Index(Guid id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                var studies = db.Studies.Include(s => s.School).Include(s => s.User).Include(s => s.Thesis).Where(s => s.IdUser == id);
                return View(studies.ToList());
            }
            else
            {
                return RedirectToAction("Begin", "User");
            }
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
                        study.IdSchool = school.Id;
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
                if (key.Contains("txtTesis"))
                {
                    Tesis.IdStudies = study.Id;
                    Tesis.Title = form[key].ToString();
                    study.Thesis = Tesis;
                }
                if (key.Contains("txtDescripcion"))
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
            return RedirectToAction("Index");
        }
       
        //
        // GET: /Study/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            Study study = db.Studies.Find(id);
            ViewBag.IdSchool = new SelectList(db.Schools, "Id", "Name", study.IdSchool);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", study.IdUser);
            ViewBag.Id = new SelectList(db.Theses, "IdStudies", "Title", study.Id);
            var electives = study.Electives;
            for (int i = electives.Count; i < 5;  i++)
            {
                electives.Add(new Elective());
            }

            return View(study);
        }

        //
        // POST: /Study/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, Study study, FormCollection form)
        {
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
            
            // falta lo de programa

            last.Grade = study.Grade;

            last.Electives.Clear();
            foreach (String key in form)
            {
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
                                    if (i.Id == elective.Id) {el2 = true;}
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
            if(study.Thesis.Title != null)
            {
                tesis.IdStudies = last.Id;
                tesis.Title = study.Thesis.Title;
                tesis.Description = study.Thesis.Description;
                if (last.Thesis != null) { db.Theses.Remove(last.Thesis); }
                db.Theses.Add(tesis);
                last.Thesis = tesis;
            }            
            
            if(ModelState.IsValid)
            {
                db.Entry(last).State = EntityState.Modified;
                db.SaveChanges();
            }

            //var n = new DateTime();
            //TimeSpan t = new TimeSpan();
            //var t = n.Subtract(n);
            //var b = DateTime.TryParse("12jsdajfjsdgds", out n);
            return RedirectToAction("Index");
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
            db.Theses.Remove(study.Thesis);
            study.Thesis = null;
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