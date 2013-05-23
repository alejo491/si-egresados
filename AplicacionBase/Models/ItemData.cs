using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class ItemData
    {
        public ItemData()
        {
            this.Fields = new List<Field>();
            this.Filters = new List<Filter>();
            this.GroupOptions = new List<GroupOption>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdReport { get; set; }
        public string SQLQuey { get; set; }
        public string GraphicType { get; set; }
        public decimal ItemNumber { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }
        public virtual ICollection<GroupOption> GroupOptions { get; set; }
        public virtual Report Report { get; set; }
    }
}
