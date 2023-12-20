using CromWood.Data.Context;
using CromWood.Data.Entities.Default;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Repository.Implementation
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        public AmenityRepository(CromwoodContext context) : base(context) { }
        public async Task<IEnumerable<Amenity>> GetAmenities()
        {
            return await _context.Amenities.ToListAsync();
        }
    }
}
