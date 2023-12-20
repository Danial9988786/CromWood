using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class PropertyService: IPropertyService
    {
        private readonly IPropertyRepository _properyRepository;
        private readonly IMapper _mapper;
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _properyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<AppResponse<IEnumerable<PropertyViewModel>>> GetPropertyForList()
        {
            try
            {
                var result = await _properyRepository.GetPropertyForList();
                var mappedResult = _mapper.Map<IEnumerable<PropertyViewModel>>(result);
                return ResponseCreater<IEnumerable<PropertyViewModel>>.CreateSuccessResponse(mappedResult, "Properties loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<PropertyViewModel>>.CreateErrorResponse(null, ex.ToString());
            }

        }

        public async Task<AppResponse<PropertyModel>> GetPropertyOverView(Guid propertyId)
        {
            try
            {
                var result = await _properyRepository.GetPropertyOverView(propertyId);
                var mappedResult = _mapper.Map<PropertyModel>(result);
                return ResponseCreater<PropertyModel>.CreateSuccessResponse(mappedResult, "Property overview loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<PropertyModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddProperty(PropertyModel property)
        {
            try
            {
                var mappedProperty = _mapper.Map<Property>(property);
                mappedProperty.PropertyAmenities.ToList().ForEach(x => x.AmenityId = x.Amenity.Id);
                mappedProperty.PropertyAmenities.ToList().ForEach(x => x.Amenity=null);
                var result = await _properyRepository.AddProperty(mappedProperty);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Property added successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<PropertyInsuranceModel>> GetPropertyInsuranceDetail(Guid propertyId)
        {
            try
            {
                var result = await _properyRepository.GetPropertyInsurance(propertyId);
                var mappedResult = _mapper.Map<PropertyInsuranceModel>(result);
                return ResponseCreater<PropertyInsuranceModel>.CreateSuccessResponse(mappedResult, "Property insurance loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<PropertyInsuranceModel>.CreateErrorResponse(null, ex.ToString());
            }
        }
        public async Task<AppResponse<int>> AddModifyInsurance(PropertyInsuranceModel insurance)
        {
            try
            {
                var mappedInsurance = _mapper.Map<PropertyInsurance>(insurance);
                var result = 0 ;
                if (mappedInsurance.Id == Guid.Empty)
                {
                    result = await _properyRepository.AddInsurance(mappedInsurance);
                }
                else
                {
                    result= await _properyRepository.ModifyInsurance(mappedInsurance);
                }
                return ResponseCreater<int>.CreateSuccessResponse(result, "Property added successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<PropertyKeyModel>>> GetPropertyKeys(Guid propertyId)
        {
            try
            {
                var result = await _properyRepository.GetPropertyKeys(propertyId);
                var mappedResult = _mapper.Map<IEnumerable<PropertyKeyModel>>(result);
                return ResponseCreater<IEnumerable<PropertyKeyModel>>.CreateSuccessResponse(mappedResult, "Keys loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<PropertyKeyModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<PropertyKeyModel>> GetPropertyKey(Guid id)
        {
            try
            {
                var result = await _properyRepository.GetPropertyKey(id);
                var mappedResult = _mapper.Map<PropertyKeyModel>(result);
                return ResponseCreater<PropertyKeyModel>.CreateSuccessResponse(mappedResult, "Key loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<PropertyKeyModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyKey(PropertyKeyModel key)
        {
            try
            {
                var mappedKey = _mapper.Map<PropertyKey>(key);
                var result = 0;
                if (mappedKey.Id == Guid.Empty)
                {
                    result = await _properyRepository.AddKey(mappedKey);
                }
                else
                {
                    result = await _properyRepository.ModifyKey(mappedKey);
                }
                return ResponseCreater<int>.CreateSuccessResponse(result, "Key add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public Task<AppResponse<int>> DeleteKey(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
