using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace CromWood.Data.Repository.Implementation
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(CromwoodContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Property>> GetPropertyForList(Guid filterId)
        {
            if (filterId != Guid.Empty)
            {
                var condition = await GetFilterConiditon(filterId);
                var result = await _context.Properties.Where(condition).Include(x => x.Asset).Include(x => x.PropertyType).Include(x => x.Tenancies).ThenInclude(x => x.TenancyTenants).ThenInclude(x => x.Tenant).OrderByDescending(x => x.CreatedDate).ToListAsync();
                return result;
            }
            return await _context.Properties.Include(x => x.Asset).Include(x => x.PropertyType).Include(x=>x.Tenancies).ThenInclude(x=>x.TenancyTenants).ThenInclude(x=>x.Tenant).OrderByDescending(x=>x.CreatedDate).ToListAsync();
        }

        public async Task<Property> GetPropertyOverView(Guid PropertyId)
        {
            return await _context.Properties.Include(x => x.Asset).Include(x => x.PropertyAmenities).ThenInclude(x => x.Amenity).Include(x => x.PropertyType).Include(x=>x.Tenancies).FirstOrDefaultAsync(x => x.Id == PropertyId);
        }

        public async Task<PropertyInsurance> GetPropertyInsurance(Guid PropertyId)
        {
            return await _context.PropertyInsurances.FirstOrDefaultAsync(x => x.PropertyId == PropertyId);
        }

        public async Task<Guid> AddModifyProperty(Property Property)
        {
            try
            {
                var changelog = new ChangeLog();
                if (Property.Id == Guid.Empty)
                {
                    Property.Id = Guid.NewGuid();
                    await _context.Properties.AddAsync(Property);

                    changelog = new ChangeLog()
                    {
                        Type = ChangeType.Add,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = Property.Id,
                        Description = $"Added New Property with Property ID: {Property.PropertyCode}"
                    };
                }
                else
                {
                    var old = await _context.Properties.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Property.Id);
                    _context.Update(Property);

                    List<string> changedProperties = ObjectComparer.CompareObjects(old, Property);
                    changelog = new ChangeLog()
                    {
                        Type = ChangeType.Edit,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = Property.Id,
                        Description = $"Changed Property Folowing were changed: {string.Join(", ", changedProperties)}"
                    };
                }
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                    await AddChangeLog(changelog);
                return Property.Id;
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
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Add,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = insurance.PropertyId,
                        Description = $"Added New Insurance to Property"
                    });
                }
                return result;
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
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Edit,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = insurance.PropertyId,
                        Description = $"Modified Insurance of the Property"
                    });
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PropertyKey>> GetPropertyKeys(Guid propertyId)
        {
            return await _context.PropertyKeys.Where(x => x.PropertyId == propertyId).Include(x=>x.PropertyKeyType).ToListAsync();
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
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Add,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = key.PropertyId,
                        Description = $"Added New Key to Property: {key.Name}"
                    });
                }
                return result;
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
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Edit,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = key.PropertyId,
                        Description = $"Updated Key of the Property: {key.Name}"
                    });
                }
                return result;
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
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    await AddChangeLog(new ChangeLog()
                    {
                        Type = ChangeType.Delete,
                        ScreenName = ScreenConstant.PropertyManagement,
                        ScreenDetailId = key.PropertyId,
                        Description = $"Deleted Key of the Property: {key.Name}"
                    });
                }
                return key.ImageUrl;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Tenancy>> GetPropertyTenancy(Guid id)
        {
            return await _context.Tenancies.Where(x=>x.PropertyId==id).Include(x=>x.TenancyTenants).ThenInclude(y=>y.Tenant).ToListAsync();
        }
    }
}
