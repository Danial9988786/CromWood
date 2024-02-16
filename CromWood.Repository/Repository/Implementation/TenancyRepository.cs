using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class TenancyRepository : Repository<Tenancy>, ITenancyRepository
    {
        public TenancyRepository(CromwoodContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Tenancy>> GetTenancyForList()
        {
            return await _context.Tenancies.Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.Property).ThenInclude(x => x.PropertyType).Include(x => x.RentFrequency).ToListAsync();
        }
        public async Task<IEnumerable<Tenancy>> GetHousingBenefitTenancy()
        {
            return await _context.Tenancies.Where(x => x.TenancyTypeId == OtherConstants.HousingBenefitTypeId).Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.Property).ThenInclude(x => x.PropertyType).Include(x => x.RentFrequency).Include(x => x.TenancyTenants).ThenInclude(x => x.Tenant).ToListAsync();
        }
        public async Task<IEnumerable<Tenancy>> GetTenancyForExport()
        {
            return await _context.Tenancies.Include(x => x.ContractType).Include(x => x.TransactionType).Include(x => x.TenancyType).Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.Property).ThenInclude(x => x.PropertyType).Include(x => x.RentFrequency).Include(x => x.PaymentMethod).ToListAsync();
        }

        public async Task<Tenancy> GetTenancyOverView(Guid tenancyId)
        {
            return await _context.Tenancies.Include(x => x.TenancyTenants).ThenInclude(x => x.Tenant).Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.Property).ThenInclude(x => x.PropertyType).Include(x => x.RentFrequency).Include(x => x.TenancyType).FirstOrDefaultAsync(x => x.Id == tenancyId);
        }

        public async Task<int> DeleteTenancyTenant(Guid id, Guid tenancyId)
        {
            try
            {
                var key = await _context.TenancyTenants.AsNoTracking().FirstOrDefaultAsync(x => x.TenantId == id && x.TenancyId == tenancyId);
                _context.TenancyTenants.Remove(key);
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Tenancy> GetTenancyViewDetail(Guid tenancyId)
        {
            return await _context.Tenancies.FirstOrDefaultAsync(x => x.Id == tenancyId);
        }

        public async Task<int> AddTenancy(Tenancy tenancy)
        {
            try
            {
                await _context.Tenancies.AddAsync(tenancy);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> EditTenancy(Tenancy tenancy)
        {
            try
            {
                _context.Tenancies.Update(tenancy);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<Tenant>> GetTenancyTenants()
        {
            return await _context.TenancyTenants.Select(x => x.Tenant).ToListAsync();
        }

        public async Task<ICollection<Tenant>> GetTenancyTenants(Guid tenancyId)
        {
            return await _context.TenancyTenants.Where(x => x.TenancyId == tenancyId).Select(x => x.Tenant).ToListAsync();
        }

        public async Task<int> LinkTenancyTenant(TenancyTenant tenancyTenant)
        {
            try
            {
                _context.TenancyTenants.Update(tenancyTenant);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<TenancyNote>> GetTenancyNotes(Guid tenancyId)
        {
            return await _context.TenancyNotes.Where(x => x.TenancyId == tenancyId).ToListAsync();
        }

        public async Task<TenancyNote> GetTenancyNote(Guid id)
        {
            return await _context.TenancyNotes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddNote(TenancyNote note)
        {
            try
            {
                note.Id = Guid.NewGuid();
                await _context.TenancyNotes.AddAsync(note);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ModifyNote(TenancyNote note)
        {
            try
            {
                _context.TenancyNotes.Update(note);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteNote(Guid id)
        {
            try
            {
                var note = await _context.TenancyNotes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.TenancyNotes.Remove(note);
                await _context.SaveChangesAsync();
                return note.FileUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<TenancyDocument>> GetTenancyDocuments(Guid tenancyId)
        {
            return await _context.TenancyDocuments.Where(x => x.TenancyId == tenancyId).ToListAsync();
        }

        public async Task<TenancyDocument> GetTenancyDocument(Guid id)
        {
            return await _context.TenancyDocuments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddDocument(TenancyDocument document)
        {
            try
            {
                document.Id = Guid.NewGuid();
                await _context.TenancyDocuments.AddAsync(document);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ModifyDocument(TenancyDocument document)
        {
            try
            {
                _context.TenancyDocuments.Update(document);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteDocument(Guid id)
        {
            try
            {
                var document = await _context.TenancyDocuments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.TenancyDocuments.Remove(document);
                await _context.SaveChangesAsync();
                return document.DocUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<Notice>> GetNoticesForTenancyTenants(Guid tenenacyId)
        {
            return await _context.Notices.Include(x => x.Concern).Include(x => x.Section).Include(x => x.Tenant).Where(x => x.Tenant.TenancyTenants.Any(x => x.TenancyId == tenenacyId)).ToListAsync();
        }

        public async Task<ICollection<Complaint>> GetComplaintsForTenancyTenants(Guid tenenacyId)
        {
            return await _context.Complaints.Include(x => x.ComplaintCategory).Include(x => x.ComplaintNature).Include(x => x.Tenant).Where(x => x.Tenant.TenancyTenants.Any(x => x.TenancyId == tenenacyId)).ToListAsync();
        }
        public async Task<ICollection<TenancyMessage>> GetTenancyMessages(Guid id)
        {
            return await _context.TenancyMessages.Where(x => x.TenancyId == id).ToListAsync();

        }
        public async Task<TenancyMessage> GetTenancyMessage(Guid messageId)
        {
            return await _context.TenancyMessages.FirstOrDefaultAsync(x => x.Id == messageId);
        }
        public async Task<int> AddModifyMessage(TenancyMessage message)
        {
            try
            {
                if (message.Id == Guid.Empty)
                {
                    await _context.TenancyMessages.AddAsync(message);
                }
                else
                {
                    _context.TenancyMessages.Update(message);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> DeleteMessage(Guid messageId)
        {
            try
            {
                var document = await _context.TenancyMessages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == messageId);
                _context.TenancyMessages.Remove(document);
                await _context.SaveChangesAsync();
                return document.AttachmentUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<UnitUtility>> GetUnitUtilities(Guid id)
        {
            return await _context.UnitUtilities.Include(x => x.UtilityProvider).Include(x => x.UtilityType).Include(x => x.UnitUtilityReadings).Where(x => x.TenancyId == id).ToListAsync();
        }

        public async Task<UnitUtility> GetUnitUtilityView(Guid id)
        {
            return await _context.UnitUtilities.Include(x => x.UtilityProvider).Include(x => x.UtilityType).Include(x => x.UnitUtilityReadings).Include(x => x.UnitUtilityDocuments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UnitUtility> GetUnitUtility(Guid id)
        {
            return await _context.UnitUtilities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyUnitUtility(UnitUtility req)
        {
            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.UnitUtilities.AddAsync(req);
                }
                else
                {
                    _context.UnitUtilities.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteUnitUtility(Guid id)
        {
            try
            {
                var utility = await _context.UnitUtilities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.UnitUtilities.Remove(utility);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UnitUtilityReading> GetUnitUtilityReading(Guid id)
        {
            return await _context.UnitUtilityReadings.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<int> AddModifyUnitUtilityReading(UnitUtilityReading req)
        {
            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.UnitUtilityReadings.AddAsync(req);
                }
                else
                {
                    _context.UnitUtilityReadings.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteUnitUtilityReading(Guid id)
        {
            try
            {
                var utility = await _context.UnitUtilityReadings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.UnitUtilityReadings.Remove(utility);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UnitUtilityDocument> GetUnitUtilityDocument(Guid id)
        {
            return await _context.UnitUtilityDocuments.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<int> AddModifyUnitUtilityDocument(UnitUtilityDocument req)
        {
            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.UnitUtilityDocuments.AddAsync(req);
                }
                else
                {
                    _context.UnitUtilityDocuments.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteUnitUtilityDocument(Guid id)
        {
            try
            {
                var utility = await _context.UnitUtilityDocuments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.UnitUtilityDocuments.Remove(utility);
                await _context.SaveChangesAsync();
                return utility.DocUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<RecurringCharge>> GetRecurringCharges(Guid id)
        {
            return await _context.RecurringCharges.Include(x => x.Tenancy).Include(x => x.RecurringBasisFor).Include(x => x.TransactionType).Include(x => x.RecurringFrequency).Where(x => x.TenancyId == id).ToListAsync();
        }

        public async Task<RecurringCharge> GetRecurringCharge(Guid id)
        {
            return await _context.RecurringCharges.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RecurringCharge> GetRecurringChargeView(Guid id)
        {
            return await _context.RecurringCharges.Include(x => x.Tenancy).Include(x => x.TransactionType).Include(x => x.RecurringFrequency).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyRecurringCharge(RecurringCharge req)
        {
            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.RecurringCharges.AddAsync(req);
                }
                else
                {
                    _context.RecurringCharges.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteRecurringCharge(Guid id)
        {
            try
            {
                var item = await _context.RecurringCharges.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.RecurringCharges.Remove(item);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<TenancyStatement>> GetStatements()
        {
            return await _context.Statements.Include(x => x.StatementType).ToListAsync();
        }

        public async Task<ICollection<TenancyStatement>> GetStatements(Guid id)
        {
            return await _context.Statements.Include(x => x.StatementType).Where(x => x.TenancyId == id).ToListAsync();
        }

        public async Task<TenancyStatement> GetStatement(Guid id)
        {
            return await _context.Statements.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<TenancyStatement>> GetHousingBenefitStatments()
        {
            return await _context.Statements.Where(x => x.StatementTypeId == OtherConstants.HousingBenefitStatementType).Include(x=>x.Tenancy).ThenInclude(x=>x.Property).ToListAsync();
        }

        public async Task<int> AddModifyStatement(TenancyStatement req)
        {
            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.Statements.AddAsync(req);
                }
                else
                {
                    _context.Statements.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddModifyBulkStatement(List<TenancyStatement> req)
        {
            try
            {
                await _context.Statements.AddRangeAsync(req);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TenancyStatement> GetStatementView(Guid id)
        {
            return await _context.Statements.Include(x => x.StatementType).Include(x => x.Transactions).Include(x => x.Documents).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> DeleteStatement(Guid id)
        {
            try
            {
                var item = await _context.Statements.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.Statements.Remove(item);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<StatementTransaction> GetStatementTransaction(Guid id)
        {
            return await _context.StatementTransactions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyStatementTransaction(StatementTransaction req)
        {
            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.StatementTransactions.AddAsync(req);
                }
                else
                {
                    _context.StatementTransactions.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteStatementTransaction(Guid id)
        {
            try
            {
                var item = await _context.StatementTransactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.StatementTransactions.Remove(item);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<StatementDocument> GetStatementDocument(Guid id)
        {
            return await _context.StatementDocuments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyStatementDocument(StatementDocument req)
        {

            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.StatementDocuments.AddAsync(req);
                }
                else
                {
                    _context.StatementDocuments.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteStatementDocument(Guid id)
        {
            try
            {
                var doc = await _context.StatementDocuments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.StatementDocuments.Remove(doc);
                await _context.SaveChangesAsync();
                return doc.DocUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ICollection<PaymentPlan>> GetPaymentPlans()
        {
            return await _context.PaymentPlans.Include(x => x.ReferenceStatement).Include(x => x.InstallmentType).ToListAsync();
        }
        public async Task<ICollection<PaymentPlan>> GetPaymentPlans(Guid id)
        {
            return await _context.PaymentPlans.Include(x => x.ReferenceStatement).Where(x => x.ReferenceStatement.TenancyId == id).Include(x => x.InstallmentType).ToListAsync();
        }

        public async Task<PaymentPlan> GetPaymentPlan(Guid id)
        {
            return await _context.PaymentPlans.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyPaymentPlan(PaymentPlan req)
        {
            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.PaymentPlans.AddAsync(req);
                }
                else
                {
                    _context.PaymentPlans.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaymentPlan> GetPaymentPlanView(Guid id)
        {
            return await _context.PaymentPlans.Include(x => x.ReferenceStatement).ThenInclude(x => x.Tenancy).ThenInclude(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.Installments).Include(x => x.Transactions).Include(x => x.InstallmentType).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> DeletePaymentPlan(Guid id)
        {
            try
            {
                var item = await _context.PaymentPlans.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.PaymentPlans.Remove(item);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaymentPlanTransaction> GetPaymentPlanTransaction(Guid id)
        {
            return await _context.PaymentPlanTransactions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddModifyPaymentPlanTransaction(PaymentPlanTransaction req)
        {
            try
            {
                if (req.Id == Guid.Empty)
                {
                    await _context.PaymentPlanTransactions.AddAsync(req);
                }
                else
                {
                    _context.PaymentPlanTransactions.Update(req);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeletePaymentPlanTransaction(Guid id)
        {
            try
            {
                var item = await _context.PaymentPlanTransactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.PaymentPlanTransactions.Remove(item);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
