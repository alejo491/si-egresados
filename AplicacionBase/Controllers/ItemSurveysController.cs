using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionBase.Models;
using iTextSharp.text;

namespace AplicacionBase.Controllers
{
    public class ItemSurveysController : Controller
    {

        private DbSIEPISContext db = new DbSIEPISContext();
        //
        // GET: /ItemSurveys/
        #region index
        DbSIEPISContext _db = new DbSIEPISContext();

        public ActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }

        public ActionResult Create(Guid id)
        {
            return View();
        }
#endregion
        [HttpPost]
        public ActionResult Create(FormCollection postedForm,Guid id)
        {

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
            //String SQL = "" + "select distinct Cantidad,(Convert(VARCHAR(2000), answerChoicesSentence)) as Respuesta from  VistaModuloEncuesta  inner JOIN  (select count(*) as cantidad,idAnswerChoices from " + tabla + " where surveysName in('" + surveysName + "') and topicsDescription in('" + tema + "') and idQuestion in('" + idQuestion + "') group by idAnswerChoices ) as cdcd on cdcd.idAnswerChoices=VistaModuloEncuesta.idAnswerChoices";
            String SQL = "" + "select (Convert(VARCHAR(2000), answerChoicesSentence)) as 'Opcion de respuesta', count(*) as Cantidad from  " + tabla + " where surveysName in('" + surveysName + "') and topicsDescription in('" + tema + "') and IdQuestion in('" + idQuestion + "') group by IdAnswer ,(Convert(VARCHAR(2000), answerChoicesSentence)) ";
            ItemSurvey itenSurvey=new ItemSurvey();
            itenSurvey.Id = Guid.NewGuid();
            itenSurvey.IdReport = id;       //se recibe como parametro antes de entrar a crear. aqui se saca en ves de crearlo
            itenSurvey.Question = question;
            itenSurvey.GraphicType = conGrafico;
            itenSurvey.ItemNumber = numPagina;
            itenSurvey.Report = db.Reports.First(a => a.Id == itenSurvey.IdReport);
            itenSurvey.SQLQuey = SQL;


            db.ItemSurveys.Add(itenSurvey);
            db.SaveChanges();

            return View();
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
