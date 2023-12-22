using CromWood.Data.Entities;
using System.Collections.Generic;

namespace CromWood.Data.Repository.Interface
{
    public interface IPropertyRepository
    {
        public Task<IEnumerable<Property>> GetPropertyForList();
        public Task<Property> GetPropertyOverView(Guid propertyId);
        public Task<PropertyInsurance> GetPropertyInsurance(Guid propertyId);
        public Task<int> AddProperty(Property property);
        public Task<int> AddInsurance(PropertyInsurance insurance);
        public Task<int> ModifyInsurance(PropertyInsurance insurance);
        public Task<IEnumerable<PropertyKey>> GetPropertyKeys(Guid propertyId);
        public Task<PropertyKey> GetPropertyKey(Guid id);
        public Task<int> AddKey(PropertyKey key);
        public Task<int> ModifyKey(PropertyKey key);
        public Task<string> DeleteKey(Guid id);

    }
}
