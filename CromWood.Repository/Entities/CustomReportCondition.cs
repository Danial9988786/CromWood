namespace CromWood.Data.Entities
{
    public class CustomReportCondition: DBTable
    {
        public Guid Id { get; set; }
        public Guid CustomReportId { get; set; }
        public CustomReport CustomReport { get; set; }
        public string Condition { get; set; }
        public string Condition1 { get; set; }
        public string Condition2 { get; set; }
        public string Condition3 { get; set; }
        public string Condition4 { get; set; }

    }
}
