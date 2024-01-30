using CromWood.Business.Helper;
using CromWood.Data.Entities;

namespace CromWood.Business.Services.Interface
{
    public interface IChangeLogService
    {
        public Task<AppResponse<IEnumerable<ChangeLog>>> GetChangeLogsForScreen(string screenName);
        public Task<AppResponse<IEnumerable<ChangeLog>>> GetChangeLogsForScreenDetail(Guid screenDetailId);
    }
}
