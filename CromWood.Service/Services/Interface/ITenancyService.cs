using CromWood.Business.Helper;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Models;

namespace CromWood.Business.Services.Interface
{
    public interface ITenancyService
    {
        public Task<AppResponse<IEnumerable<TenancyViewModel>>> GetTenancyForList();
        public Task<AppResponse<TenancyModel>> GetTenancyOverView(Guid tenancyId);
        public Task<AppResponse<TenancyViewModel>> GetTenancyViewDetail(Guid tenancyId);
        public Task<AppResponse<int>> AddTenancy(TenancyModel tenancy);
        public Task<AppResponse<int>> EditTenancy(TenancyModel tenancy);
        public Task<AppResponse<IEnumerable<TenantViewModel>>> GetTenancyTenants(Guid tenancyId);
        public Task<AppResponse<int>> LinkTenancyTenant(TenancyTenantModel tenancyTenant);
        public Task<AppResponse<IEnumerable<TenancyNoteModel>>> GetTenancyNotes(Guid tenancyId);
        public Task<AppResponse<TenancyNoteModel>> GetTenancyNote(Guid id);
        public Task<AppResponse<int>> AddModifyNote(TenancyNoteModel note);
        public Task<AppResponse<int>> DeleteNote(Guid id);
        public Task<AppResponse<IEnumerable<TenancyDocumentModel>>> GetTenancyDocuments(Guid tenancyId);
        public Task<AppResponse<TenancyDocumentModel>> GetTenancyDocument(Guid id);
        public Task<AppResponse<int>> AddModifyDocument(TenancyDocumentModel document);
        public Task<AppResponse<int>> DeleteDocument(Guid id);
        public Task<AppResponse<ICollection<NoticeViewModel>>> GetNoticesForTenancyTenants(Guid tenancyId);
        public Task<AppResponse<ICollection<ComplaintViewModel>>> GetComplaintsForTenancyTenants(Guid tenancyId);
    }
}
