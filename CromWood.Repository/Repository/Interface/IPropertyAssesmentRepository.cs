using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IPropertyAssesmentRepository
    {
        public Task<IEnumerable<PropertyAssesment>> GetPropertyAssesments();
        public Task<PropertyAssesment> GetPropertyAssesment(Guid assesmentId);
        public Task<IEnumerable<PropertyAssesment>> GetPropertyAssesmentsOfProperty(Guid propId);
        public Task<int> AddModifyPropertyAssesment(PropertyAssesment assesment);
        public Task<IEnumerable<PropertyInspectionItem>> GetInspectionItems(Guid assesmentId);
        public Task<int> AddModifyPropertyAssesmentItem(PropertyInspectionItem item);
        public Task<IEnumerable<PropertyInspectionItemImage>> GetBuildingImages(Guid assesmentId);
        public Task<int> AddModifyPropertyAssesmentImage(PropertyInspectionItemImage item);
    }
}
