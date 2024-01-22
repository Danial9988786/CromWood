using CromWood.Data;
using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class PropertyInspectionItemViewModel
    {
        public Guid Id { get; set; }
        // Location related
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public SurverySection SurverySection { get; set; }
        public DetailItem DetailItem { get; set; }
        public string Description { get; set; }
        public int ConditionRating { get; set; }
        public string Defects { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public float StockUnitCost { get; set; }
        public int StockQuantity { get; set; }
        public int StockRemainingQuantity { get; set; }
        public int StockReplaceYear { get; set; }
        public string StockYearBand { get; set; }
        public int StockLifecycle { get; set; }
        public ICollection<PropertyInspectionItemImageViewModel> PropertyInspectionItemImages { get; set; }

    }
    public class PropertyInspectionItemImageViewModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
