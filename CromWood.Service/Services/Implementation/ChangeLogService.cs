using CromWood.Business.Helper;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class ChangeLogService : IChangeLogService
    {
        public readonly IRepository<ChangeLog> repository;

        public ChangeLogService(IRepository<ChangeLog> repo)
        {
            repository = repo;
        }
        public async Task<AppResponse<IEnumerable<ChangeLog>>> GetChangeLogsForScreen(string screenName)
        {
            try
            {
                var result = await repository.GetChangeLogsForScreen(screenName);
                return ResponseCreater<IEnumerable<ChangeLog>>.CreateSuccessResponse(result, "Change logs loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<ChangeLog>>.CreateErrorResponse(new List<ChangeLog> { }, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<ChangeLog>>> GetChangeLogsForScreenDetail(Guid screenDetailId)
        {
            try
            {
                var result = await repository.GetChangeLogsForScreenDetail(screenDetailId);
                return ResponseCreater<IEnumerable<ChangeLog>>.CreateSuccessResponse(result, "Change logs loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<ChangeLog>>.CreateErrorResponse(new List<ChangeLog> { }, ex.ToString());
            }
        }
    }
}
