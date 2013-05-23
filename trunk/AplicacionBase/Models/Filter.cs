namespace AplicacionBase.Models
{
    public partial class Filter
    {
        public System.Guid Id { get; set; }
        public System.Guid IdItemData { get; set; }
        public string FieldName { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string LogicOperator { get; set; }
        public decimal FilterNumber { get; set; }
        public virtual ItemData ItemData { get; set; }
    }
}
