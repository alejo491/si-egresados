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

        public ActionResult Fill(Guid ids)
        {
            var survey = db.Surveys.Find(ids);
            if (survey != null)
            {
                var surveystopics = db.SurveysTopics.Where(st => st.IdSurveys == survey.Id).OrderBy(st=>st.TopicNumber);
                foreach (var surveysTopic in surveystopics.ToList())
                {
                    var topic = db.Topics.Find(surveysTopic.IdTopic);
                    //var user = db.aspnet_Users.Find(idu);
                    //var model = new AnswerViewModel(db.Questions.Where(s=>s.IdTopic == topic.Id));
                    if (topic != null)
                    {
                        var question = db.Questions.Where(s => s.IdTopic == topic.Id).OrderBy(s => s.QuestionNumber);
                        foreach (var question1 in question.ToList())
                        {
                            Question question2 = question1;
                            var answers = db.AnswerChoices.Where(a => a.IdQuestion == question2.Id).OrderByDescending(s => s.Type);
                            question1.AnswerChoices = answers.ToList();
                        }

                        surveysTopic.Topic = topic;
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    
                }

                ViewBag.questions = surveystopics.ToList();
                return View();
            }

            return RedirectToAction("Index", "Home");
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
