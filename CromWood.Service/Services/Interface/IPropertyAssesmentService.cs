using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;

namespace CromWood.Business.Services.Interface
{
    public interface IPropertyAssesmentService
    {
        public Task<AppResponse<IEnumerable<PropertyAssesmentViewModel>>> GetPropertyAssesments();
        public Task<AppResponse<PropertyAssesmentViewModel>> GetPropertyAssesment(Guid assesmentId);
        public Task<AppResponse<IEnumerable<PropertyAssesmentViewModel>>> GetPropertyAssesmentsOfProperty(Guid propId);
        public Task<AppResponse<int>> AddModifyPropertyAssesment(PropertyAssesmentModel propertyAssesment);
        public Task<AppResponse<IEnumerable<PropertyInspectionItemViewModel>>> GetInspectionItems(Guid assesmentId);
        public Task<AppResponse<int>> AddModifyPropertyAssesmentItem(PropertyInspectionItemModel item);
        public Task<AppResponse<IEnumerable<PropertyInspectionItemImageViewModel>>> GetBuildingImages(Guid assesmentId);
        public Task<AppResponse<int>> AddModifyPropertyAssesmentImage(PropertyInspectionItemImageModel item);
    }
}
