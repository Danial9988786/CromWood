namespace CromWood.Data.Entities
{
    // Message Recipient are basically Tenants right now
    public class MessageRecipient : DBTable
    {
        public Guid Id { get; set; }
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
        public Guid RecipientId { get; set; }
        public Tenant Recipient { get; set; }
    }
}
