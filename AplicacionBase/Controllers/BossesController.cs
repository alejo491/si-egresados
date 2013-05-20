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

namespace AplicacionBase.Controllers
{
    [Authorize]
    public class BossesController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        public ActionResult Index()
        {            
            return View(db.Bosses.ToList());
        }

        //
        // GET: /Bosses/Details/5

        public ActionResult Details(Guid id)
        {
            Boss boss= db.Bosses.Find(id);
            return View(boss);
        }

        //
        // GET: /Bosses/Create

        public ActionResult Create()
        {            
            return View();
        } 

        //
        // POST: /Bosses/Create

        [HttpPost]
        public ActionResult Create(Boss boss)
        {
            if (ModelState.IsValid)
            {
                //string nombre = HttpContext.cu .User.Identity
                boss.Id = Guid.NewGuid();
                db.Bosses.Add(boss);
                db.SaveChanges();
                return RedirectToAction("Create", "Bosses");
            }
            return View();            
        }
        
        //
        // GET: /Bosses/Edit/5

        public ActionResult Edit(Guid id)
        {
            Boss boss= db.Bosses.Find(id);
            return View(boss);
        }

        //
        // POST: /Bosses/Edit/5

        [HttpPost]
        public ActionResult Edit(Boss boss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boss);
        }

        //
        // GET: /Bosses/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            Boss boss = db.Bosses.Find(id);
            return View(boss);
        }

        //
        // POST: /Bosses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Boss boss= db.Bosses.Find(id);
            db.Bosses.Remove(boss);
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
