﻿using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface ITenancyRepository
    {
        public Task<IEnumerable<Tenancy>> GetTenancyForList();
        public Task<IEnumerable<Tenancy>> GetHousingBenefitTenancy();
        public Task<IEnumerable<Tenancy>> GetTenancyForExport();
        public Task<Tenancy> GetTenancyOverView(Guid tenancyId);
        public Task<int> DeleteTenancyTenant(Guid id, Guid tenancyId);
        public Task<Tenancy> GetTenancyViewDetail(Guid tenancyId);
        public Task<int> AddTenancy(Tenancy tenancy);
        public Task<int> EditTenancy(Tenancy tenancy);
        public Task<ICollection<Tenant>> GetTenancyTenants();
        public Task<ICollection<Tenant>> GetTenancyTenants(Guid tenancyId);
        public Task<int>  LinkTenancyTenant(TenancyTenant tenancyTenant);
        public Task<ICollection<TenancyNote>> GetTenancyNotes(Guid tenancyId);
        public Task<TenancyNote> GetTenancyNote(Guid id);
        public Task<int> AddNote(TenancyNote note);
        public Task<int> ModifyNote(TenancyNote note);
        public Task<string> DeleteNote(Guid id);
        public Task<ICollection<TenancyDocument>> GetTenancyDocuments(Guid tenancyId);
        public Task<TenancyDocument> GetTenancyDocument(Guid id);
        public Task<int> AddDocument(TenancyDocument document);
        public Task<int> ModifyDocument(TenancyDocument document);
        public Task<string> DeleteDocument(Guid id);
        public Task<ICollection<Notice>> GetNoticesForTenancyTenants(Guid tenenacyId);
        public Task<ICollection<Complaint>> GetComplaintsForTenancyTenants(Guid tenenacyId);
        public Task<ICollection<TenancyMessage>> GetTenancyMessages(Guid id);
        public Task<TenancyMessage> GetTenancyMessage(Guid messageId);
        public Task<int> AddModifyMessage(TenancyMessage message);
        public Task<string> DeleteMessage(Guid messageId);
        public Task<ICollection<UnitUtility>> GetUnitUtilities(Guid id);
        public Task<UnitUtility> GetUnitUtility(Guid id);
        public Task<UnitUtility> GetUnitUtilityView(Guid id);
        public Task<int> AddModifyUnitUtility(UnitUtility req);
        public Task<int> DeleteUnitUtility(Guid id);
        public Task<UnitUtilityReading> GetUnitUtilityReading(Guid id);
        public Task<int> AddModifyUnitUtilityReading(UnitUtilityReading req);
        public Task<int> DeleteUnitUtilityReading(Guid id);
        public Task<UnitUtilityDocument> GetUnitUtilityDocument(Guid id);
        public Task<int> AddModifyUnitUtilityDocument(UnitUtilityDocument req);
        public Task<string> DeleteUnitUtilityDocument(Guid id);
        public Task<ICollection<RecurringCharge>> GetRecurringCharges(Guid id);
        public Task<RecurringCharge> GetRecurringCharge(Guid id);
        public Task<RecurringCharge> GetRecurringChargeView(Guid id);
        public Task<int> AddModifyRecurringCharge(RecurringCharge mapped);
        public Task<int> DeleteRecurringCharge(Guid id);
        public Task<ICollection<TenancyStatement>> GetStatements();
        public Task<ICollection<TenancyStatement>> GetStatements(Guid id);
        public Task<ICollection<TenancyStatement>> GetHousingBenefitStatments();
        public Task<TenancyStatement> GetStatement(Guid id);
        public Task<int> AddModifyStatement(TenancyStatement mapped);
        public Task<int> AddModifyBulkStatement(List<TenancyStatement> mapped);
        public Task<TenancyStatement> GetStatementView(Guid id);
        public Task<int> DeleteStatement(Guid id);
        public Task<StatementTransaction> GetStatementTransaction(Guid id);
        public Task<int> AddModifyStatementTransaction(StatementTransaction mappedMessage);
        public Task<int> DeleteStatementTransaction(Guid id);
        public Task<StatementDocument> GetStatementDocument(Guid id);
        public Task<int> AddModifyStatementDocument(StatementDocument mappedReq);
        public Task<string> DeleteStatementDocument(Guid id);
        public Task<ICollection<PaymentPlan>> GetPaymentPlans();
        public Task<ICollection<PaymentPlan>> GetPaymentPlans(Guid id);
        public Task<PaymentPlan> GetPaymentPlan(Guid id);
        public Task<int> AddModifyPaymentPlan(PaymentPlan mapped);
        public Task<PaymentPlan> GetPaymentPlanView(Guid id);
        public Task<int> DeletePaymentPlan(Guid id);
        public Task<PaymentPlanTransaction> GetPaymentPlanTransaction(Guid id);
        public Task<int> AddModifyPaymentPlanTransaction(PaymentPlanTransaction mappedMessage);
        public Task<int> DeletePaymentPlanTransaction(Guid id);
    }
}
