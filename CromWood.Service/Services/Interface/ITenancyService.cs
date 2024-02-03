using CromWood.Business.Helper;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Models;

namespace CromWood.Business.Services.Interface
{
    public interface ITenancyService
    {
        public Task<AppResponse<IEnumerable<TenancyViewModel>>> GetTenancyForList();
        public Task<AppResponse<IEnumerable<TenancyModel>>> GetTenancyForExport();
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
        public Task<AppResponse<ICollection<TenancyMessageViewModel>>> GetTenancyMessages(Guid id);
        public Task<AppResponse<TenancyMessageModel>> GetTenancyMessage(Guid messageId);
        public Task<AppResponse<int>> AddModifyMessage(TenancyMessageModel message);
        public Task<AppResponse<int>> DeleteMessage(Guid messageId);
        public Task<AppResponse<ICollection<UnitUtilityViewModel>>> GetUnitUtilities(Guid id);
        public Task<AppResponse<UnitUtilityModel>> GetUnitUtility(Guid id);
        public Task<AppResponse<UnitUtilityViewModel>> GetUnitUtilityView(Guid id);
        public Task<AppResponse<int>> AddModifyUnitUtility(UnitUtilityModel req);
        public Task<AppResponse<int>> DeleteUnitUtility(Guid id);
        public Task<AppResponse<UnitUtilityReadingModel>> GetUnitUtilityReading(Guid id);
        public Task<AppResponse<int>> AddModifyUnitUtilityReading(UnitUtilityReadingModel req);
        public Task<AppResponse<int>> DeleteUnitUtilityReading(Guid id);
        public Task<AppResponse<UnitUtilityDocumentModel>> GetUnitUtilityDocument(Guid id);
        public Task<AppResponse<int>> AddModifyUnitUtilityDocument(UnitUtilityDocumentModel req);
        public Task<AppResponse<int>> DeleteUnitUtilityDocument(Guid id);
        public Task<AppResponse<ICollection<RecurringChargeViewModel>>> GetRecurringCharges(Guid id);
        public Task<AppResponse<RecurringChargeModel>> GetRecurringCharge(Guid id);
        public Task<AppResponse<RecurringChargeViewModel>> GetRecurringChargeView(Guid id);
        public Task<AppResponse<int>> AddModifyRecurringCharge(RecurringChargeModel req);
        public Task<AppResponse<int>> DeleteRecurringCharge(Guid id);
        public Task<AppResponse<ICollection<StatementViewModel>>> GetStatements(Guid id);
        public Task<AppResponse<StatementModel>> GetStatement(Guid id);
        public Task<AppResponse<int>> AddModifyStatement(StatementModel req);
        public Task<AppResponse<StatementViewModel>> GetStatementView(Guid id);
        public Task<AppResponse<int>> DeleteStatement(Guid id);
        public Task<AppResponse<StatementTransactionModel>> GetStatementTransaction(Guid id);
        public Task<AppResponse<int>> AddModifyStatementTransaction(StatementTransactionModel req);
        public Task<AppResponse<int>> DeleteStatementTransaction(Guid id);
        public Task<AppResponse<StatementDocumentModel>> GetStatementDocument(Guid id);
        public Task<AppResponse<int>> AddModifyStatementDocument(StatementDocumentModel req);
        public Task<AppResponse<int>> DeleteStatementDocument(Guid id);
    }
}
