using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        public TenantRepository(CromwoodContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tenant>> GetTenantForList()
        {
            return await _context.Tenants.ToListAsync();
        }
        public async Task<IEnumerable<Tenant>> GetTenantsNotInTenancy(Guid tenancyId)
        {
            return await _context.Tenants.Include(x=>x.TenancyTenants).Where(x=>!x.TenancyTenants.Any(x=>x.TenancyId== tenancyId)).ToListAsync();
        }

        public async Task<IEnumerable<Tenancy>> GetTenanciesForTenant(Guid tenancyId)
        {
            var tenancyTenants = _context.Tenants.Where(x=>x.Id==tenancyId).SelectMany(x => x.TenancyTenants.Select(y=>y.Tenancy)).Distinct();
            return await tenancyTenants.ToListAsync();
        }


        public async Task<Tenant> GetTenantOverView(Guid tenancyId)
        {
            return await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenancyId);
        }

        public async Task<int> AddModifyTenant(Tenant tenant)
        {
            try
            {
                if (tenant.Id == Guid.Empty) { _context.Tenants.Add(tenant); } else { _context.Tenants.Update(tenant); };
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> EditTenant(Tenant tenancy)
        {
            try
            {
                _context.Tenants.Update(tenancy);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
