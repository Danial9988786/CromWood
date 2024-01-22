namespace CromWood.Data.Entities
{
    public class TenancyTenant: DBTable
    {
        public Guid Id { get; set; }
        public Guid TenancyId { get; set; }
        public Tenancy Tenancy { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
