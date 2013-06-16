using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AplicacionBase.Models.ViewModels
{
    public class ItemReportViewModel
    {
        public Guid Id { get; set; }
        public Guid IdReporte { get; set; }
        public string Reporte { get; set; }        
        public string Sentence { get; set; }
        public string GraphicType { get; set; }
        public int ItemNumber { get; set; }
        public List<Dictionary<string, int>> DataList { get; set; }
        public int DataNumber { get; set; }
        public bool Type { get; set; }
        public DataTable Table { get; set; }
        public ItemReportViewModel()
        {
            Sentence = "";
            GraphicType = "";
            ItemNumber = 0;
            DataNumber = 0;
            DataList = new List<Dictionary<string, int>>();
            Table = new DataTable();
        }
    }
}