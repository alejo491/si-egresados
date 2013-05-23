namespace AplicacionBase.Models
{
    public partial class ItemSurvey
    {
        public System.Guid Id { get; set; }
        public System.Guid IdReport { get; set; }
        public string Question { get; set; }
        public string GraphicType { get; set; }
        public decimal ItemNumber { get; set; }
        public string SQLQuey { get; set; }
        public virtual Report Report { get; set; }
    }
}
