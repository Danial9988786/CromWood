namespace CromWood.Data.Entities
{
    public class TenancyNote
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public string FileUrl { get; set; }
        public bool ScheduleForLater { get; set; }
        public DateTime? ScheduleDateTime { get; set; }
        public Guid TenancyId { get; set; }
        public Tenancy Tenancy { get; set; }
    }
}
