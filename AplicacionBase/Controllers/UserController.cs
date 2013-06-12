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
    public class UserController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /User/

        public ViewResult Index()
        {
            var users = db.Users.Include(u => u.aspnet_Users);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", users.ToList());
            return View(users.ToList());
        }

        public ActionResult Begin(Guid id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return View();
            }
            else
            {
                ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
                return View(user);
            }
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(Guid id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.UserName = HttpContext.User.Identity.Name;
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName");
            return View();
        }

        //
        // POST: /User/Create
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

        [HttpPost]
        public ActionResult Create(User user)
        {
            Guid g = searchId();
            if (ModelState.IsValid && !g.Equals(System.Guid.Empty))
            {
                //string nombre = HttpContext.cu .User.Identity
                user.Id = g;
                user.States = "true";
                db.Users.Add(user);
                db.SaveChanges();
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
                //return RedirectToAction("Index", "Home");
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }

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

        //
        // GET: /User/Edit/5

        public ActionResult Edit(Guid id)
        {
            User user = db.Users.Find(id);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            //return RedirectToAction("Index", "User");
            return View(user);
        }

        //
        // POST: /User/Edit/5

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


        public ActionResult State(Guid id)
        {
            User user = db.Users.Find(id);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult State(User user)
        {
            if (user != null)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Estado"] = "¡ Estado del Usuario Modificado Correctamente !";
                return RedirectToAction("Index", "User");
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Intento de registrar al usuario
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
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        public ActionResult Generate(Guid id)
        {
            User user = db.Users.Find(id);
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName");
            return View(user);
        }

        [HttpPost]
        public ActionResult Generate(User user)
        {
            //Guid h = System.Guid.Empty;
            if (ModelState.IsValid)
            {
                //user.Id = h;
                user.States = "true";
                db.Users.Add(user);
                db.SaveChanges();
                TempData["Creado"] = "¡ El Usuario se Creó Correctamente !";
                return RedirectToAction("Index", "User");
            }
            ViewBag.Id = new SelectList(db.aspnet_Users, "UserId", "UserName", user.Id);
            return View(user);
        }

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


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /User/Delete/5
        /*
                public ActionResult Delete(Guid id)
                {
                    User user = db.Users.Find(id);
                    return View(user);
                }

                //
                // POST: /User/Delete/5

                [HttpPost, ActionName("Delete")]
                public ActionResult DeleteConfirmed(Guid id)
                {
                    User user = db.Users.Find(id);
                    //aspnet_Users w = db.aspnet_Users.Find(id);
                    foreach (var ca in db.aspnet_Roles)
                    {
                        aspnet_UsersInRoles v = db.aspnet_UsersInRoles.Find(user.Id, ca.RoleId);
                        if (v != null)
                        {
                            v.aspnet_Roles = null;
                            v.aspnet_Users = null;
                            db.aspnet_UsersInRoles.Remove(v);
                        }
                    }
                    db.Users.Remove(user);
                    //db.aspnet_Users.Remove(w);
                    db.SaveChanges();
                    TempData["Eliminado"] = "¡ Usuario Eliminado Correctamente !";
                    return RedirectToAction("Index");
                }
        */
    }
}
