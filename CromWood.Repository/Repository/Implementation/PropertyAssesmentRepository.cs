using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class PropertyAssesmentRepository : Repository<PropertyAssesment>, IPropertyAssesmentRepository
    {
        public PropertyAssesmentRepository(CromwoodContext context) : base(context) { }

        public async Task<IEnumerable<PropertyAssesment>> GetPropertyAssesments(Guid filterId)
        {
            if (filterId != Guid.Empty)
            {
                var condition = await GetFilterConiditon(filterId);
                var result = await _context.PropertyAssesments.Include(x => x.Property).ThenInclude(x => x.Asset).ToListAsync();
                return result;
            }
            return await _context.PropertyAssesments.Include(x=>x.Property).ThenInclude(x=>x.Asset).ToListAsync();
        }

        public async Task<PropertyAssesment> GetPropertyAssesment(Guid assesmentId)
        {
            return await _context.PropertyAssesments.Include(x => x.Property).ThenInclude(x => x.Asset).Include(x => x.Property).ThenInclude(x=>x.PropertyType).FirstOrDefaultAsync(x => x.Id == assesmentId);
        }

        public async Task<IEnumerable<PropertyAssesment>> GetPropertyAssesmentsOfProperty(Guid propId)
        {
            return await _context.PropertyAssesments.Where(x => x.PropertyId == propId).ToListAsync();
        }

        public async Task<int> AddModifyPropertyAssesment(PropertyAssesment propertyAssesment)
        {
            try
            {
                if (propertyAssesment.Id == Guid.Empty)
                {
                    propertyAssesment.Id = Guid.NewGuid();
                    await _context.PropertyAssesments.AddAsync(propertyAssesment);
                }
                else
                {
                    _context.PropertyAssesments.Update(propertyAssesment);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PropertyInspectionItem>> GetInspectionItems(Guid assesmentId)
        {
            // Incase of need all Inspection items
            if(assesmentId == Guid.Empty)
            {
                return await _context.PropertyInspectionItems.Include(x => x.DetailItem).ToListAsync();
            }
            return await _context.PropertyInspectionItems.Include(x => x.DetailItem).Include(x => x.UnitOfMeasurement).Include(x => x.SurverySection).Include(x => x.PropertyInspectionItemImages).Where(x => x.PropertyAssesmentId == assesmentId).ToListAsync();
        }

        public async Task<int> AddModifyPropertyAssesmentItem(PropertyInspectionItem item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                    await _context.PropertyInspectionItems.AddAsync(item);
                }
                else
                {
                    _context.PropertyInspectionItems.Update(item);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PropertyInspectionItemImage>> GetBuildingImages(Guid assesmentId)
        {
            return await _context.PropertyInspectionItemImage.Where(x => x.PropertyAssesmentId == assesmentId).ToListAsync();
        }

        public async Task<int> AddModifyPropertyAssesmentImage(PropertyInspectionItemImage item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                    await _context.PropertyInspectionItemImage.AddAsync(item);
                }
                else
                {
                    _context.PropertyInspectionItemImage.Update(item);
                }
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
