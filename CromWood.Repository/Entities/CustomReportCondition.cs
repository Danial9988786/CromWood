namespace CromWood.Data.Entities
{
    public class CustomReportCondition: DBTable
    {
        public Guid Id { get; set; }
        public Guid CustomReportId { get; set; }
        public CustomReport CustomReport { get; set; }
        public string Condition { get; set; }
    }
}
