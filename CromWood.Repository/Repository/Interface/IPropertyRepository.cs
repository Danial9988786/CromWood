using CromWood.Data.DTO;
using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IPropertyRepository
    {
        public Task<IEnumerable<Property>> GetPropertyForList(Guid filterId);
        public Task<Property> GetPropertyOverView(Guid propertyId);
        public Task<Guid> AddModifyProperty(Property property);
        public Task<PropertyInsurance> GetPropertyInsurance(Guid propertyId);
        public Task<int> AddInsurance(PropertyInsurance insurance);
        public Task<int> ModifyInsurance(PropertyInsurance insurance);
        public Task<IEnumerable<PropertyKey>> GetPropertyKeys(Guid propertyId);
        public Task<PropertyKey> GetPropertyKey(Guid id);
        public Task<int> AddKey(PropertyKey key);
        public Task<int> ModifyKey(PropertyKey key);
        public Task<string> DeleteKey(Guid id);
        public Task<IEnumerable<Tenancy>> GetPropertyTenancy(Guid id);
        public Task<IEnumerable<Complaint>> GetRecentComplaints(Guid propertyId);
        public Task<IEnumerable<TenancyStatement>> GetRecentTransactions(Guid propertyId);
        public Task<PropertyOverviewDTO> GetPropertyOverview(Guid propertyId);

    }
}
