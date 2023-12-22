using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(CromwoodContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Property>> GetPropertyForList()
        {
            return await _context.Properties.Include(x => x.Asset).Include(x => x.PropertyType).ToListAsync();
        }

        public async Task<Property> GetPropertyOverView(Guid PropertyId)
        {
            return await _context.Properties.Include(x => x.Asset).Include(x => x.PropertyAmenities).ThenInclude(x => x.Amenity).Include(x => x.PropertyType).FirstOrDefaultAsync(x => x.Id == PropertyId);
        }

        public async Task<PropertyInsurance> GetPropertyInsurance(Guid PropertyId)
        {
            return await _context.PropertyInsurances.FirstOrDefaultAsync(x => x.PropertyId == PropertyId);
        }

        public async Task<int> AddProperty(Property Property)
        {
            try
            {
                Property.Id = Guid.NewGuid();
                await _context.Properties.AddAsync(Property);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddInsurance(PropertyInsurance insurance)
        {
            try
            {
                insurance.Id = Guid.NewGuid();
                await _context.PropertyInsurances.AddAsync(insurance);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ModifyInsurance(PropertyInsurance insurance)
        {
            try
            {
                _context.PropertyInsurances.Update(insurance);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PropertyKey>> GetPropertyKeys(Guid propertyId)
        {
            return await _context.PropertyKeys.Where(x => x.PropertyId == propertyId).ToListAsync();
        }

        public async Task<PropertyKey> GetPropertyKey(Guid id)
        {
            return await _context.PropertyKeys.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddKey(PropertyKey key)
        {
            try
            {
                key.Id = Guid.NewGuid();
                await _context.PropertyKeys.AddAsync(key);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ModifyKey(PropertyKey key)
        {
            try
            {
                _context.PropertyKeys.Update(key);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteKey(Guid id)
        {
            try
            {
                var key = await _context.PropertyKeys.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                _context.PropertyKeys.Remove(key);
                await _context.SaveChangesAsync();
                return key.ImageUrl;
            }
            catch
            {
                throw;
            }
        }
    }
}
