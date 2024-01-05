namespace CromWood.Data.Entities
{
    public class TenancyDocument
    {
        public Guid Id { get; set; }
        public string DocumentName { get; set; }
        public string DocUrl { get; set; }
        public Guid TenancyId { get; set; }
        public Tenancy Tenancy { get; set; }
    }
}
