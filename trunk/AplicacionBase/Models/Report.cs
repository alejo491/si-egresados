using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class Report
    {
        public Report()
        {
            this.ItemDatas = new List<ItemData>();
            this.Items = new List<Item>();
            this.ItemSurveys = new List<ItemSurvey>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdUser { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = " ¡El campo es obligatorio!")]
        public string Description { get; set; }

        public System.DateTime ReportDate { get; set; }
        public virtual ICollection<ItemData> ItemDatas { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ItemSurvey> ItemSurveys { get; set; }
        public virtual User User { get; set; }
    }
}
