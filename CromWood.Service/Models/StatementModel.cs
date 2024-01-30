namespace CromWood.Business.Models
{
    public class StatementModel
    {
        public Guid Id { get; set; }
        public Guid StatementTypeId { get; set; }
        public string ReferenceID { get; set; }
        public float NetAmount { get; set; }
        public DateTime Date { get; set; }
        public string StatementDescription { get; set; }
        public Guid TenancyId { get; set; }
    }
}
