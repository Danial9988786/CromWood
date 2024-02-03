using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;

namespace CromWood.Business.Services.Implementation
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _properyRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploader _fileUploader;
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IFileUploader uploader)
        {
            _properyRepository = propertyRepository;
            _mapper = mapper;
            _fileUploader = uploader;
        }

        public async Task<AppResponse<IEnumerable<PropertyViewModel>>> GetPropertyForList(Guid filterId)
        {
            try
            {
                var result = await _properyRepository.GetPropertyForList(filterId);
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

        public async Task<AppResponse<Guid>> AddModifyProperty(PropertyModel property)
        {
            try
            {
                var mappedProperty = _mapper.Map<Property>(property);
                mappedProperty.PropertyAmenities.ToList().ForEach(x => x.AmenityId = x.Amenity.Id);
                mappedProperty.PropertyAmenities.ToList().ForEach(x => x.Amenity = null);
                var result = await _properyRepository.AddModifyProperty(mappedProperty);
                return ResponseCreater<Guid>.CreateSuccessResponse(result, "Property added successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<Guid>.CreateErrorResponse(Guid.Empty, ex.ToString());
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
                var result = 0;

                if (insurance.File != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedInsurance.Id != Guid.Empty && mappedInsurance.FileUrl!=null)
                    {
                        await _fileUploader.Delete(mappedInsurance.FileUrl, "propertyinsurance");
                    }
                    mappedInsurance.FileUrl = await _fileUploader.Upload(insurance.File, "propertyinsurance");
                }

                result = mappedInsurance.Id == Guid.Empty ? await _properyRepository.AddInsurance(mappedInsurance) : await _properyRepository.ModifyInsurance(mappedInsurance);

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
                if (key.ImageFile != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedKey.Id != Guid.Empty)
                    {
                        await _fileUploader.Delete(mappedKey.ImageUrl, "propertykey");
                    }
                    mappedKey.ImageUrl = await _fileUploader.Upload(key.ImageFile, "propertykey");
                }

                result = mappedKey.Id == Guid.Empty ? await _properyRepository.AddKey(mappedKey) : await _properyRepository.ModifyKey(mappedKey);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Key add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteKey(Guid id)
        {
            try
            {
                var imageUrl = await _properyRepository.DeleteKey(id);
                if (imageUrl != null) await _fileUploader.Delete(imageUrl, "propertykey");
                return ResponseCreater<int>.CreateSuccessResponse(1, "Key deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<TenancyViewModel>>> GetPropertyTenancy(Guid id)
        {
            try
            {
                var result = await _properyRepository.GetPropertyTenancy(id);
                var mappedResult = _mapper.Map<IEnumerable<TenancyViewModel>>(result);
                return ResponseCreater<IEnumerable<TenancyViewModel>>.CreateSuccessResponse(mappedResult, "Tenancies loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<TenancyViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

    }
}
