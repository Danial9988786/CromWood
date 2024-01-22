using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;

namespace CromWood.Business.Services.Interface
{
    public interface INoticeClaimsService
    {
        public Task<AppResponse<IEnumerable<NoticeViewModel>>> GetNotices(bool archived=false);
        public Task<AppResponse<NoticeModel>> GetNoticeById(Guid id);
        public Task<AppResponse<NoticeViewModel>> GetNoticeViewById(Guid id);
        public Task<AppResponse<int>> AddModifyNotice(NoticeModel notice);
        public Task<AppResponse<int>> DeleteNotice(Guid id);
        public Task<AppResponse<int>> ArchiveNotice(Guid id);

        public Task<AppResponse<IEnumerable<ClaimViewModel>>> GetClaims();
        public Task<AppResponse<ClaimViewModel>> GetClaimViewById(Guid id);
        public Task<AppResponse<ClaimModel>> GetClaimById(Guid id);
        public Task<AppResponse<int>> AddModifyClaim(ClaimModel claim);
        public Task<AppResponse<int>> DeleteClaim(Guid id);
    }
}
