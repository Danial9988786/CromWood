namespace CromWood.Data.Entities
{
    public class ComplaintComment : DBTable
    {
        public Guid Id { get; set; }
        public Guid ComplaintId { get; set; }
        public Complaint Complaint { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
    }
}
