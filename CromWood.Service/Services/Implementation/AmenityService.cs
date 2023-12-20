using CromWood.Business.Helper;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities.Default;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class AmenityService : IAmenityService
    {
        private readonly IAmenityRepository _amenityRepository;
        public AmenityService(IAmenityRepository amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }

        public async Task<AppResponse<IEnumerable<Amenity>>> GetAmenities()
        {
            try
            {
                var result = await _amenityRepository.GetAmenities();
                return ResponseCreater<IEnumerable<Amenity>>.CreateSuccessResponse(result, "Amenity loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<Amenity>>.CreateErrorResponse(null, ex.ToString());
            }
        }
    }
}
