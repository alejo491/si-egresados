using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AplicacionBase.Models;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Controllers
{
    public class FillSurveyController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();
        //
        // GET: /FillSurvey/

        public ActionResult Fill(Guid idt)
        {
            var topic = db.Topics.Find(idt);
            //var user = db.aspnet_Users.Find(idu);
            //var model = new AnswerViewModel(db.Questions.Where(s=>s.IdTopic == topic.Id));
            var question = db.Questions.Where(s => s.IdTopic == idt);
            foreach (var question1 in question.ToList())
            {
                var answers = db.AnswerChoices.Where(a => a.IdQuestion == question1.Id);
                question1.AnswerChoices = answers.ToList();
            }

            ViewBag.questions = question;
            return View();
        }
        [HttpPost]
        public ActionResult Fill(Guid idt, FormCollection postedForm)
        {
            var q = db.Questions.Where(s => s.IdTopic == idt);




            foreach (var  question in q)
            {
                var v = postedForm[question.Id.ToString()].ToString();
            }
            return View();
        }

    }
}
