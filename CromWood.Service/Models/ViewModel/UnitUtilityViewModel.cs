using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class UnitUtilityViewModel: DBTable
    {
        public Guid Id { get; set; }
        public string MeterSerialNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Note { get; set; }
        public Guid TenancyId { get; set; }
        public TenancyViewModel Tenancy { get; set; }
        public Guid UtilityTypeId { get; set; }
        public UtilityType UtilityType { get; set; }
        public Guid UtilityProviderId { get; set; }
        public UtilityProvider UtilityProvider { get; set; }

        public ICollection<UnitUtilityReadingViewModel> UnitUtilityReadings { get; set; }
        public ICollection<UnitUtilityDocumentViewModel> UnitUtilityDocuments { get; set; }
    }
}
