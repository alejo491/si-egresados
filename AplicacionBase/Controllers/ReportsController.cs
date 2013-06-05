using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using AplicacionBase.Models;
using AplicacionBase.Models.ViewModels;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using iTextSharp.text.html.simpleparser;

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

        // GET: /Reports/Generar

        public ViewResult Generar()
        {
            var selectquery = "SELECT gender, count(*) as cantidad FROM Users GROUP BY gender";
            DataSet ds = new DataSet();
            var strconn = ConfigurationManager.ConnectionStrings["DbSIEPISContext"].ToString();
            var myCon = new SqlConnection(strconn);
            myCon.Open();
            var myAda = new SqlDataAdapter(selectquery, myCon);
            myAda.Fill(ds);
            myCon.Close();

            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            document.Open();

            Paragraph parrafo = new Paragraph();

            parrafo.Alignment = Element.ALIGN_CENTER;
            parrafo.Font = FontFactory.GetFont("Arial", 24);
            parrafo.Font.SetStyle(Font.BOLD);
            parrafo.Add("Reporte de Usuarios por Sexo");

            document.Add(parrafo);

            parrafo = new Paragraph("\n");

            document.Add(parrafo);

            PdfPTable tabla = new PdfPTable(2);
            tabla.SetWidthPercentage(new float[] {100, 100}, PageSize.A4);
            tabla.AddCell(new Paragraph("Genero"));
            tabla.AddCell(new Paragraph("Cantidad"));

            foreach (PdfPCell celda in tabla.Rows[0].GetCells())
            {
                celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                celda.HorizontalAlignment = 1;
                celda.Padding = 3;
            }

            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {

                string genero = Convert.ToString(row["gender"]);
                string cantidad = Convert.ToString(row["cantidad"]);

                PdfPCell celda1 = new PdfPCell(new Paragraph(genero, FontFactory.GetFont("Arial", 10)));
                PdfPCell celda2 = new PdfPCell(new Paragraph(cantidad, FontFactory.GetFont("Arial", 10)));

                tabla.AddCell(celda1);
                tabla.AddCell(celda2);

            }

            document.Add(tabla);

            document.Close();

            Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = fileName;
            prc.Start();

            return View();
        }

        // GET: /Reports/GenerarPDF

        public void GenerarPDF(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Reporte.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 50, 50, 25, 25);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            Paragraph parrafo = new Paragraph("\n");
            pdfDoc.Add(parrafo);
            try
            {
                htmlparser.StartDocument();
                htmlparser.Parse(sr);
                htmlparser.EndDocument();
                htmlparser.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                pdfDoc.Close();
            }

            Response.Write(pdfDoc);
            Response.End();
        }


        public ActionResult Preview(Guid id)
        {
            ViewBag.IdReport = id;
            return View();
        }

        #region Codigo, para generar tabla y grafico (chart pie) de los reportes

        public ActionResult RenderReport(Guid id)
        {
            var items = new List<ItemReportViewModel>();
            var report = db.Reports.Find(id);
            ViewBag.ReportName = report.Description;
            ViewBag.ReportDate = report.ReportDate;

            if (report != null)
            {
                var itemsurveys = db.ItemSurveys.Where(item => item.IdReport == id).ToList();
                foreach (var itemsurvey in itemsurveys)
                {
                    string selectquery = itemsurvey.SQLQuey;
                    var dataa = QueryHelper.GetDataSet(selectquery);
                    var dataSet = dataa.ToXml();
                    var ds = new DataSet();
                    var stream = new StringReader(dataSet);
                    ds.ReadXml(stream);
                    DataTable dt = ds.Tables[0];
                    var item = new ItemReportViewModel();
                    var d = new Dictionary<string, int>();
                    int n = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        int temp = Int32.Parse(row.ItemArray[1].ToString());
                        d.Add(row.ItemArray[0].ToString(), temp);
                        n += temp;
                    }

                    item.DataNumber = n;
                    item.GraphicType = itemsurvey.GraphicType;
                    item.ItemNumber = (int) itemsurvey.ItemNumber;
                    item.Sentence = itemsurvey.Question;
                    item.Type = true;
                    item.Table = dt;
                    item.DataList.Add(d);
                    items.Add(item);
                }

                var itemsDatas = db.ItemDatas.Where(it => it.IdReport == id).ToList();
                foreach (var itemsData in itemsDatas)
                {
                    string selectquery = itemsData.SQLQuey;
                    var dataa = QueryHelper.GetDataSet(selectquery);
                    var dataSet = dataa.ToXml();
                    var ds = new DataSet();
                    var stream = new StringReader(dataSet);
                    ds.ReadXml(stream);
                    DataTable dt = ds.Tables[0];
                    var item = new ItemReportViewModel();
                    var d = new Dictionary<string, int>();
                    //int n = 0;
                    var labels = new List<string>();
                    var vals = new List<List<int>>();
                    if (dt.Rows.Count > 0)
                    {
                        int m = dt.Rows.Count;
                        int n = dt.Rows[0].ItemArray.Length;
                        var valores = new int[m,n];
                        var fila = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            int indice = 0;
                            string label = "";

                            foreach (var ob in row.ItemArray)
                            {

                                int value = 0;
                                if (int.TryParse(ob.ToString(), out value))
                                {
                                    valores[fila, indice] = value;
                                }
                                else
                                {
                                    label += ob.ToString() + " ";
                                }
                                indice++;
                            }
                            fila++;
                            labels.Add(label);
                        }


                        var l = new List<Dictionary<string, int>>();
                        for (int i = 0; i < n; i++)
                        {
                            var d1 = new Dictionary<string, int>();
                            for (int j = 0; j < m; j++)
                            {
                                foreach (var label in labels)
                                {
                                    d1.Add(label, valores[i, j]);
                                }
                            }
                            l.Add(d1);
                        }

                        item.DataNumber = 0;
                        item.GraphicType = itemsData.GraphicType;
                        item.ItemNumber = (int) itemsData.ItemNumber;
                        item.Sentence = itemsData.Sentence;
                        item.DataList = l;
                        item.Type = false;
                        item.Table = dt;

                    }
                    else
                    {
                        item.DataNumber = 0;
                        item.GraphicType = itemsData.GraphicType;
                        item.ItemNumber = (int) itemsData.ItemNumber;
                        item.Sentence = itemsData.Sentence;
                        item.DataList = new List<Dictionary<string, int>>();
                        item.Type = false;
                        item.Table = dt;
                    }

                    items.Add(item);
                }

            }

            var il = items.OrderBy(ite => ite.DataNumber).ToList();
            ViewBag.Items = il;
            /*var itemSurvey = db.ItemSurveys.Find(id);
            
            string selectquery = itemSurvey.SQLQuey;
            var dataa = GetDataSet(selectquery);            
            var dataSet = dataa.ToXml();
            var ds = new DataSet();
            var stream = new StringReader(dataSet);
            ds.ReadXml(stream);
            DataTable dt = ds.Tables[0];*/
            //Dictionary<string,int> d = new Dictionary<string,int>();

            /*foreach (DataRow row in dt.Rows)
            {
                d.Add(row.ItemArray[0].ToString(),Int32.Parse(row.ItemArray[1].ToString()));
            }
            foreach (var i in d)
            {
                KeyValuePair<string, int> k = i;
            }*/
            /*ViewBag.Question = itemSurvey.Question;
            ViewBag.graphicsvalue = d;
            ViewBag.TipoGrafico = itemSurvey.GraphicType;
            
            // dataSet.ReadXmlSchema(Server.MapPath("data.xsd"));
            //dataSet.ReadXml(Server.MapPath("data.xml"));
            ViewBag.datos = dataSet;*/
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

        //    DataSet GetDataSet(string sqlCommand)
        //    {

        //        DataSet ds = new DataSet();
        //        var strconn = ConfigurationManager.ConnectionStrings["DbSIEPISContext"].ToString();
        //        var myCon = new SqlConnection(strconn);
        //        myCon.Open();
        //        var myAda = new SqlDataAdapter(sqlCommand, myCon);
        //        myAda.Fill(ds);
        //        myCon.Close();
        //        //String connectionString = ConfigurationManager.ConnectionStrings["DbSIEPISContext"].ConnectionString;
        //        /*using (SqlCommand cmd = new SqlCommand(sqlCommand, new SqlConnection(connectionString)))
        //        {
        //            cmd.Connection.Open();
        //            DataTable table = new DataTable();
        //            table.Load(cmd.ExecuteReader());
        //            ds.Tables.Add(table);
        //        } * */
        //        return ds;
        //    }
        //}

        //public static class Extensions
        //{
        //    public static string ToXml(this DataSet ds)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            using (TextWriter streamWriter = new StreamWriter(memoryStream))
        //            {
        //                var xmlSerializer = new XmlSerializer(typeof(DataSet));
        //                xmlSerializer.Serialize(streamWriter, ds);
        //                return Encoding.UTF8.GetString(memoryStream.ToArray());
        //            }
        //        }
        //    }
        //}

        #endregion

    }
}