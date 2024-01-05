using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface ITenantRepository
    {
        public Task<IEnumerable<Tenant>> GetTenantForList();
        public Task<IEnumerable<Tenant>> GetTenantsNotInTenancy(Guid tenancyId);
        public Task<IEnumerable<Tenancy>> GetTenanciesForTenant(Guid tenancyId);
        public Task<Tenant> GetTenantOverView(Guid tenancyId);
        public Task<int> AddModifyTenant(Tenant tenancy);
        public Task<int> EditTenant(Tenant tenancy);
    }
}
