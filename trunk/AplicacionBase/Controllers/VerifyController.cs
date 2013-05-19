﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
namespace AplicacionBase.Controllers
{
    public class VerifyController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        //
        // GET: /Verify/
        
        public ActionResult Index()
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {
                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }
            }
            foreach (var e2 in db.Users)
            {
                if (e2.Id == g)
                {
                    var steps = db.UsersSteps.Where(s=>s.UserId==e2.Id).OrderBy(s=>s.Step.SOrder);

                    if (!steps.Any())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var tmp = (List<UsersStep>)steps.ToList();
                        Session["steps"] = tmp;
                        var ActualStep = Convert.ToInt16(tmp.ElementAt(0).Step.SOrder);
                        return RedirectToAction("Index", "Wizard", ActualStep);
                    }

                    
                }
            }
            return RedirectToAction("Create", "User");
        }
        
        public ActionResult Edit()
        {
            Guid g = System.Guid.Empty;
            foreach (var e in db.aspnet_Users)
            {
                if (e.UserName == HttpContext.User.Identity.Name)
                {
                    g = e.UserId;
                }
            }
            foreach (var e2 in db.Users)
            {
                if (e2.Id == g)
                {
                    return RedirectToAction("Edit", "User", new { id = g });
                }
            }
            return RedirectToAction("Create", "User");
        }

        
        
    }

}
