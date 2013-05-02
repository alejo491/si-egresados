using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class Report
    {
        public Report()
        {
            this.Items = new List<Item>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdUser { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public System.DateTime ReportDate { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual User User { get; set; }
    }
}
