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
    public class ItemSurveysController : Controller
    {

        private DbSIEPISContext db = new DbSIEPISContext();
        //
        // GET: /ItemSurveys/
        #region index
        DbSIEPISContext _db = new DbSIEPISContext();

        public ActionResult Index(Guid id)
        {
            //var itemsurveys = db.ItemSurveys.Include(i => i.Report);
            var itemsurveys = db.ItemSurveys.Where(i=>i.IdReport==id);
            return View(itemsurveys.ToList());
        }

        public ActionResult Create(Guid id)
        {
            return View();
        }
#endregion
        [HttpPost]
        public ActionResult Create(FormCollection postedForm,Guid id)
        {
            var tam = postedForm.Count;
            if (tam > 4) { 
            var conGrafico = postedForm[3];
            int numPagina = 0;
            try
            {
                 numPagina = Convert.ToInt32(postedForm[4]);
            }
            catch(Exception e) {
                numPagina = 0;
            }
            Guid idSurvey = Guid.Empty;
            Guid idTopic = Guid.Empty;
            Guid idQuestion = Guid.Empty;
            string question = "";
            foreach (string v in postedForm)
            {
                if (v.StartsWith("surveysfield")) {
                    idSurvey=new Guid(postedForm[v]);
                    
                }
                if (v.StartsWith("topicsfield"))
                {
                    idTopic=new Guid(postedForm[v]);
                }
                if (v.StartsWith("questionfield"))
                {
                    idQuestion=new Guid(postedForm[v]);

                }
             
            }
            string tema = ((Topic)db.Topics.First(t => t.Id == idTopic)).Description; 
            string surveysName = ((Survey)db.Surveys.First(f => f.Id == idSurvey)).Name;
            question = ((Question)(db.Questions.First(st => st.Id == idQuestion))).Sentence;
            string topic = "" + db.Topics.Where(st => st.Id== idTopic);
            string tabla = "VistaModuloEncuesta";
            String SQL = "" + "select * from " + tabla + " where surveysName='" + surveysName + "' and topicsDescription='" + tema + "' and questionsSentence='" + question + "'";
            ItemSurvey itenSurvey=new ItemSurvey();
            itenSurvey.Id = Guid.NewGuid();
            itenSurvey.IdReport = id;       //se recibe como parametro antes de entrar a crear. aqui se saca en ves de crearlo
            itenSurvey.Question = question;
            itenSurvey.GraphicType = conGrafico;
            itenSurvey.ItemNumber = numPagina;
            itenSurvey.Report = db.Reports.First(a => a.Id == itenSurvey.IdReport);
            itenSurvey.SQLQuey = SQL;

if(numPagina!=0){
            db.ItemSurveys.Add(itenSurvey);
            db.SaveChanges();
    }
            }
            //return View();
            return RedirectToAction("Index", new { id = id });
        }
        //
        // GET: /itemsurveys/Details/5

        public ViewResult Details(Guid id)
        {
            ItemSurvey itemsurvey = db.ItemSurveys.Find(id);
            return View(itemsurvey);
        }


        //
        // GET: /itemsurveys/Delete/5

        public ActionResult Delete(Guid id)
        {
            ItemSurvey itemsurvey = db.ItemSurveys.Find(id);
            return View(itemsurvey);
        }

        //
        // POST: /itemsurveys/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ItemSurvey itemsurvey = db.ItemSurveys.Find(id);
            db.ItemSurveys.Remove(itemsurvey);
            db.SaveChanges();
            TempData["Success"] = "Se ha Eliminado el Item correctamente";
            return RedirectToAction("Index", new { id = itemsurvey.IdReport });
        }
        #region utilidades
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

        #endregion
    }
}
