using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class TenancyViewModel
    {
        public Guid Id { get; set; }
        public string TenancyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float RentAmount { get; set; }
        public RentFrequency RentFrequency { get; set; }
        public PropertyViewModel Property { get; set; }
    }
}
