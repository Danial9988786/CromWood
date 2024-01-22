using Microsoft.AspNetCore.Http;

namespace CromWood.Business.Models
{
    public class PropertyInspectionItemModel
    {
        public Guid Id { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public Guid? SurverySectionId { get; set; }
        public Guid? DetailItemId { get; set; }
        public string Description { get; set; }
        public string ConditionRating { get; set; }
        public string Defects { get; set; }
        public Guid? UnitOfMeesurementId { get; set; }
        public float StockUnitCost { get; set; }
        public int StockQuantity { get; set; }
        public int StockRemainingQuantity { get; set; }
        public int StockReplaceYear { get; set; }
        public string StockYearBand { get; set; }
        public int StockLifecycle { get; set; }
        public Guid PropertyAssesmentId { get; set; }
        public ICollection<IFormFile> Images { get; set; }
        public ICollection<PropertyInspectionItemImageModel> PropertyInspectionItemImages { get; set; }
    }

    public class PropertyInspectionItemImageModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Guid? PropertyInspectionItemId { get; set; }
        public Guid? PropertyAssesmentId { get; set; }
        public IFormFile Image { get; set; }

    }
}
