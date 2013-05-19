using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    public class ItemSurveysController : Controller
    {
        //
        // GET: /ItemSurveys/
        
        DbSIEPISContext _db = new DbSIEPISContext();

        public ActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult SurveysList()
        {
            //List<String> surveys = new List<string>();
            //surveys.Add("Suma");
            //surveys.Add("Minimo");
            //surveys.Add("Maximo");
            //surveys.Add("Contar");
            //surveys.Add("Promedio");
            List<Survey> surveys = _db.Surveys.ToList();
            var d = new Dictionary<string, string>();
            foreach (var survey in surveys)
            {
                d.Add(survey.Id.ToString(),survey.Name);
            }        
            return Json(d, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TopicsList(string ids)
        {
            var g = new Guid(ids);
            var topics = _db.SurveysTopics.Where(s => s.IdSurveys == g).ToList();
            var list = new Dictionary<string, string>();
            foreach (var surveysTopic in topics)
            {
                var topic = _db.Topics.Find(surveysTopic.IdTopic);
                list.Add(topic.Id.ToString(), topic.Description);

            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QuestionsList(string idt)
        {
            var g = new Guid(idt);
            var questions = _db.Questions.Where(s => s.IdTopic == g);
            var list = new Dictionary<string, string>();
            foreach (var question in questions)
            {
                list.Add(question.Id.ToString(), question.Sentence);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
}
