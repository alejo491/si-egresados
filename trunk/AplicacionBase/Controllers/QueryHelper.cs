using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using AplicacionBase.Models.ViewModels;

namespace AplicacionBase.Controllers
{
    public static class QueryHelper
    {

        #region Codigo, para generar tabla y grafico (chart pie) de los reportes

        public static DataSet GetDataSet(string sqlCommand)
        {
            
            DataSet ds = new DataSet();
            var strconn = ConfigurationManager.ConnectionStrings["DbSIEPISContext"].ToString();
            var myCon = new SqlConnection(strconn);
            myCon.Open();
            var myAda = new SqlDataAdapter(sqlCommand, myCon);
            myAda.Fill(ds);
            myCon.Close();
 
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
