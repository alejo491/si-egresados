

namespace AplicacionBase.Models
{
    public partial class Field
    {
        public System.Guid Id { get; set; }
        public System.Guid IdItemData { get; set; }
        public string FieldName { get; set; }
        public string FieldOperation { get; set; }
        public decimal FieldOrder { get; set; }
        public virtual ItemData ItemData { get; set; }
    }
}
