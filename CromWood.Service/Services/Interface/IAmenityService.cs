using CromWood.Business.Helper;
using CromWood.Data.Entities.Default;

namespace CromWood.Business.Services.Interface
{
    public interface IAmenityService
    {
        public Task<AppResponse<IEnumerable<Amenity>>> GetAmenities();

    }
}
