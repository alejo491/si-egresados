namespace AplicacionBase.Models
{
    public partial class GroupOption
    {
        public System.Guid Id { get; set; }
        public System.Guid IdItemData { get; set; }
        public string FieldName { get; set; }
        public decimal GruopOrder { get; set; }
        public virtual ItemData ItemData { get; set; }
    }
}
