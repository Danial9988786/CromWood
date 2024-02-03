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
    public class PropertyAssesmentService : IPropertyAssesmentService
    {
        private readonly IPropertyAssesmentRepository _properyAssesmentRepository;

        public readonly IMapper _mapper;
        public readonly IFileUploader _uploader;

        public PropertyAssesmentService(IPropertyAssesmentRepository assesmentRepository, IMapper mapper, IFileUploader fileUploader)
        {
            _properyAssesmentRepository = assesmentRepository;
            _mapper = mapper;
            _uploader= fileUploader;
        }

        public async Task<AppResponse<IEnumerable<PropertyAssesmentViewModel>>> GetPropertyAssesments(Guid filterId)
        {
            try
            {
                var result = await _properyAssesmentRepository.GetPropertyAssesments(filterId);
                var mappedResult = _mapper.Map<IEnumerable<PropertyAssesmentViewModel>>(result);
                return ResponseCreater<IEnumerable<PropertyAssesmentViewModel>>.CreateSuccessResponse(mappedResult, "Assesments loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<PropertyAssesmentViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }


        public async Task<AppResponse<PropertyAssesmentViewModel>> GetPropertyAssesment(Guid assesmentId)
        {
            try
            {
                var result = await _properyAssesmentRepository.GetPropertyAssesment(assesmentId);
                var mappedResult = _mapper.Map<PropertyAssesmentViewModel>(result);
                return ResponseCreater<PropertyAssesmentViewModel>.CreateSuccessResponse(mappedResult, "Assesment loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<PropertyAssesmentViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<PropertyAssesmentModel>> GetPropertyAssesmentById(Guid assesmentId)
        {
            try
            {
                var result = await _properyAssesmentRepository.GetPropertyAssesment(assesmentId);
                var mappedResult = _mapper.Map<PropertyAssesmentModel>(result);
                return ResponseCreater<PropertyAssesmentModel>.CreateSuccessResponse(mappedResult, "Assesment loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<PropertyAssesmentModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<PropertyAssesmentViewModel>>> GetPropertyAssesmentsOfProperty(Guid propId)
        {
            try
            {
                var result = await _properyAssesmentRepository.GetPropertyAssesmentsOfProperty(propId);
                var mappedResult = _mapper.Map<IEnumerable<PropertyAssesmentViewModel>>(result);
                return ResponseCreater<IEnumerable<PropertyAssesmentViewModel>>.CreateSuccessResponse(mappedResult, "Assesments loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<PropertyAssesmentViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyPropertyAssesment(PropertyAssesmentModel propertyAssesment)
        {
            try
            {
                var mapped = _mapper.Map<PropertyAssesment>(propertyAssesment);
                var result = await _properyAssesmentRepository.AddModifyPropertyAssesment(mapped);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Property Assesment add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<PropertyInspectionItemViewModel>>> GetInspectionItems(Guid assesmentId)
        {
            try
            {
                var result = await _properyAssesmentRepository.GetInspectionItems(assesmentId);
                var mappedResult = _mapper.Map<IEnumerable<PropertyInspectionItemViewModel>>(result);
                return ResponseCreater<IEnumerable<PropertyInspectionItemViewModel>>.CreateSuccessResponse(mappedResult, "Assesments items loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<PropertyInspectionItemViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyPropertyAssesmentItem(PropertyInspectionItemModel item)
        {
            try
            {
                var mapped = _mapper.Map<PropertyInspectionItem>(item);
                if(item.Images != null)
                {
                    // Upload Files & get URL.
                    foreach(var image in item.Images)
                    {
                        var url = await _uploader.Upload(image, "propertyassesment");
                        mapped.PropertyInspectionItemImages.Add(new PropertyInspectionItemImage()
                        {
                            Url = url
                        });
                    }
                }
                var result = await _properyAssesmentRepository.AddModifyPropertyAssesmentItem(mapped);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Property Assesment item add/update successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<PropertyInspectionItemImageViewModel>>> GetBuildingImages(Guid assesmentId)
        {
            try
            {
                var result = await _properyAssesmentRepository.GetBuildingImages(assesmentId);
                var mappedResult = _mapper.Map<IEnumerable<PropertyInspectionItemImageViewModel>>(result);
                return ResponseCreater<IEnumerable<PropertyInspectionItemImageViewModel>>.CreateSuccessResponse(mappedResult, "Assesments items loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<PropertyInspectionItemImageViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyPropertyAssesmentImage(PropertyInspectionItemImageModel item)
        {
            try
            {
                var mapped = _mapper.Map<PropertyInspectionItemImage>(item);
                if (item.Image != null)
                {
                        mapped.Url = await _uploader.Upload(item.Image, "propertyassesment");
                }
                var result = await _properyAssesmentRepository.AddModifyPropertyAssesmentImage(mapped);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Property Assesment image add/update successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

    }
}
