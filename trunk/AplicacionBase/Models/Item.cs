using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Item
    {
        public System.Guid Id { get; set; }
        public System.Guid IdReport { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public decimal PageNumber { get; set; }
        public virtual Report Report { get; set; }
    }
}
