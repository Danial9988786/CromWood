using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface ITenancyRepository
    {
        public Task<IEnumerable<Tenancy>> GetTenancyForList();
        public Task<Tenancy> GetTenancyOverView(Guid tenancyId);
        public Task<Tenancy> GetTenancyViewDetail(Guid tenancyId);
        public Task<int> AddTenancy(Tenancy tenancy);
        public Task<int> EditTenancy(Tenancy tenancy);
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
    }
}
