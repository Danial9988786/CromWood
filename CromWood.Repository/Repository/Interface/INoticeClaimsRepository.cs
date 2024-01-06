using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface INoticeClaimsRepository
    {
        public Task<IEnumerable<Notice>> GetNotices(bool archived=false);
        public Task<Notice> GetNoticeById(Guid id);
        public Task<int> AddModifyNotice(Notice notice);
        public Task<int> DeleteNotice(Guid id);
        public Task<int> ArchiveNotice(Guid id);
        public Task<IEnumerable<Claim>> GetClaims();
        public Task<Claim> GetClaimById(Guid id);
        public Task<int> AddModifyClaim(Claim claim);
        public Task<int> DeleteClaim(Guid id);
    }
}
