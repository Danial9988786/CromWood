using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models
{
    public class CapitalCostForecast
    {
        public DetailItem Item { get; set; }
        public List<CapitalCostComponent> YearlyCost { get; set; }
    }
}
