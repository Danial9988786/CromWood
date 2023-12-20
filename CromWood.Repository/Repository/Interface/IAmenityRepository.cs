using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;

namespace CromWood.Data.Repository.Interface
{
    public interface IAmenityRepository
    {
        public Task<IEnumerable<Amenity>> GetAmenities();
    }
}
