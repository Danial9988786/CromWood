using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Models;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Business.Services.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class TenantService: ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        public TenantService(ITenantRepository tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<AppResponse<IEnumerable<TenantViewModel>>> GetTenantForList()
        {
            try
            {
                var result = await _tenantRepository.GetTenantForList();
                var mappedResult = _mapper.Map<IEnumerable<TenantViewModel>>(result);
                return ResponseCreater<IEnumerable<TenantViewModel>>.CreateSuccessResponse(mappedResult, "Tenancies loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<TenantViewModel>>.CreateErrorResponse(null, ex.ToString());
            }

        }

        public async Task<AppResponse<IEnumerable<TenantViewModel>>> GetTenantsNotInTenancy(Guid tenancyId)
        {
            try
            {
                var result = await _tenantRepository.GetTenantsNotInTenancy(tenancyId);
                var mappedResult = _mapper.Map<IEnumerable<TenantViewModel>>(result);
                return ResponseCreater<IEnumerable<TenantViewModel>>.CreateSuccessResponse(mappedResult, "Tenants not in tenancy loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<TenantViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<TenantViewModel>> GetTenantViewModel(Guid tenantId)
        {
            try
            {
                var result = await _tenantRepository.GetTenantOverView(tenantId);
                var mappedResult = _mapper.Map<TenantViewModel>(result);
                return ResponseCreater<TenantViewModel>.CreateSuccessResponse(mappedResult, "Tenant view loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<TenantViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<TenantModel>> GetTenantOverView(Guid tenantId)
        {
            try
            {
                var result = await _tenantRepository.GetTenantOverView(tenantId);
                var mappedResult = _mapper.Map<TenantModel>(result);
                return ResponseCreater<TenantModel>.CreateSuccessResponse(mappedResult, "Tenant overview loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<TenantModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<TenancyViewModel>>> GetTenanciesForTenant(Guid tenantId)
        {
            try
            {
                var result = await _tenantRepository.GetTenanciesForTenant(tenantId);
                var mappedResult = _mapper.Map<IEnumerable<TenancyViewModel>>(result);
                return ResponseCreater<IEnumerable<TenancyViewModel>>.CreateSuccessResponse(mappedResult, "Tenant Tenancies loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<TenancyViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }


        public async Task<AppResponse<int>> AddModifyTenant(TenantModel tenant)
        {
            try
            {
                var mappedTenant = _mapper.Map<Tenant>(tenant);
                var result = await _tenantRepository.AddModifyTenant(mappedTenant);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Tenant added/modified successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> EditTenant(TenantModel tenant)
        {
            try
            {
                var mappedTenant = _mapper.Map<Tenant>(tenant);
                var result = await _tenantRepository.EditTenant(mappedTenant);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Tenant edited successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }
    }
}
