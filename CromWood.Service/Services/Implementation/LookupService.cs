using CromWood.Business.Helper;
using CromWood.Business.Services.Interface;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class LookupService<T> : ILookupService<T> where T : class
    {
        private ILookupRepository<T> _repository;
        public LookupService(ILookupRepository<T> lookupRepository) {
        _repository = lookupRepository;
        }
        public async Task<AppResponse<IEnumerable<T>>> GetAllItems()
        {
            try { 
                var response = await _repository.GetAllAsync();
                return ResponseCreater<IEnumerable<T>>.CreateSuccessResponse(response, "Lookups loaded successfully");
            }

            catch(Exception ex) { 
                return ResponseCreater<IEnumerable<T>>.CreateErrorResponse(null, ex.ToString());
            }
        }
    }
}
