namespace CromWood.Data.Entities
{
    public class TenancyMessage : DBTable
    {
        public Guid Id { get; set; }
        public Guid TenancyId { get; set; }
        public Tenancy Tenancy { get; set; }
        public string Medium { get; set; } // Medium Can be wither Whatsapp or Email
        public string Subject { get; set; }
        public string Message { get; set; }
        public string AttachmentUrl { get; set; }
        public bool? IsDraft { get; set; }
    }
}
