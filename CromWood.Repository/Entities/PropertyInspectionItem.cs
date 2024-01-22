using CromWood.Data.Entities.Default;

namespace CromWood.Data.Entities
{
    public class PropertyInspectionItem : DBTable
    {
        public Guid Id { get;set; }
        // Location related
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        // Details
        public Guid?  SurverySectionId { get; set; }
        public SurverySection SurverySection { get; set; }
        public Guid?  DetailItemId { get; set; }
        public DetailItem DetailItem { get; set; }
        public string Description { get; set; }
        public int ConditionRating { get; set; } // 0 ,1 , 2 => POOR FAIR GOOD
        public string Defects { get; set; }
        // Capital costs
        public Guid? UnitOfMeasurementId { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public float StockUnitCost { get; set; }
        public int StockQuantity { get; set; }
        public int StockRemainingQuantity { get; set; }
        public int StockReplaceYear { get; set; }
        public string StockYearBand { get; set; }
        public int StockLifecycle { get; set; }
        public Guid PropertyAssesmentId { get; set; }
        public PropertyAssesment PropertyAssesment { get; set; }
        public ICollection<PropertyInspectionItemImage> PropertyInspectionItemImages { get; set; }

    }
}
