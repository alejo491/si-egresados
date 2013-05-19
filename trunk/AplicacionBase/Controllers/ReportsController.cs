using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using AplicacionBase.Models;

namespace AplicacionBase.Controllers
{
    public class ReportsController : Controller
    {
        private DbSIEPISContext db = new DbSIEPISContext();

        //
        // GET: /Reports/

        public ViewResult Index()
        {
            var te = db.aspnet_Users.First(u => u.UserName == HttpContext.User.Identity.Name).UserId;
            var reports = db.Reports.Where(r => r.IdUser == te);
            return View(reports.ToList());
        }

        //
        // GET: /Reports/Details/5

        public ViewResult Details(Guid id)
        {
            Report report = db.Reports.Find(id);
            var user = db.Users.Find(report.IdUser);

           // ViewBag.Re = id;

            ViewBag.nombre = user.FirstNames + " " + user.LastNames;

            return View(report);
        }

        //
        // GET: /Reports/Create

        public ActionResult Create()
        {
            //ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber");
            return View();
        }

        //
        // POST: /Reports/Create

        [HttpPost]
        public ActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                report.IdUser = db.aspnet_Users.First(u => u.UserName == HttpContext.User.Identity.Name).UserId;
                report.ReportDate = DateTime.Now;
                report.Id = Guid.NewGuid();
                db.Reports.Add(report);
                db.SaveChanges();
                TempData["Create"] = "Se registró correctamente el reporte !";
                return RedirectToAction("Index");
            }

            return View(report);
        }

        //
        // GET: /Reports/Edit/5

        public ActionResult Edit(Guid id)
        {
            Report report = db.Reports.Find(id);

            return View(report);
        }

        //
        // POST: /Reports/Edit/5

        [HttpPost]
        public ActionResult Edit(Report report)
        {
            if (ModelState.IsValid)
            {
                report.IdUser = db.aspnet_Users.First(u => u.UserName == HttpContext.User.Identity.Name).UserId;
                report.ReportDate = DateTime.Now;
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Update"] = "Se actualizó correctamente el reporte !";
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Users, "Id", "PhoneNumber", report.IdUser);
            return View(report);
        }

        //
        // GET: /Reports/Delete/5

        public ActionResult Delete(Guid id)
        {
            Report report = db.Reports.Find(id);
            return View(report);
        }

        //
        // POST: /Reports/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            db.SaveChanges();
            TempData["Delete"] = "Se eliminó correctamente el reporte !";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    #region Codigo, para generar tabla y grafico (chart pie) de los reportes

        public ActionResult Preview()
        {
            var selectquery = "SELECT * FROM Topics";
            var dataa = GetDataSet(selectquery);
            var dataSet = dataa.ToXml();
           // dataSet.ReadXmlSchema(Server.MapPath("data.xsd"));
            //dataSet.ReadXml(Server.MapPath("data.xml"));
            ViewBag.datos = dataSet;
            return View();
        }
/*
        private static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(query);

            String constr = ConfigurationManager.ConnectionStrings["mydbase"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }
*/

        DataSet GetDataSet(string sqlCommand)
        {
            
            DataSet ds = new DataSet();
            var strconn = ConfigurationManager.ConnectionStrings["DbSIEPISContext"].ToString();
            var myCon = new SqlConnection(strconn);
            myCon.Open();
            var myAda = new SqlDataAdapter(sqlCommand, myCon);
            myAda.Fill(ds);
            myCon.Close();
            //String connectionString = ConfigurationManager.ConnectionStrings["DbSIEPISContext"].ConnectionString;
            /*using (SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(connectionString)))
            {
                cmd.Connection.Open();
                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);
            } * */
            return ds;
        }
    }

    public static class Extensions
    {
        public static string ToXml(this DataSet ds)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(DataSet));
                    xmlSerializer.Serialize(streamWriter, ds);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }
    }
    #endregion

}