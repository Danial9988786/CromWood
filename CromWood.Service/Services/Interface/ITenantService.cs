using CromWood.Business.Helper;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Models;

namespace CromWood.Business.Services.Interface
{
    public interface ITenantService
    {
        public Task<AppResponse<IEnumerable<TenantViewModel>>> GetTenantForList();
        public Task<AppResponse<IEnumerable<TenantViewModel>>> GetTenantsNotInTenancy(Guid tenancyId);
        public Task<AppResponse<TenantViewModel>> GetTenantViewModel(Guid TenantId);
        public Task<AppResponse<TenantModel>> GetTenantOverView(Guid TenantId);
        public Task<AppResponse<IEnumerable<TenancyViewModel>>> GetTenanciesForTenant(Guid TenantId);
        public Task<AppResponse<int>> AddModifyTenant(TenantModel Tenant);
        public Task<AppResponse<int>> EditTenant(TenantModel Tenant);
    }
}
