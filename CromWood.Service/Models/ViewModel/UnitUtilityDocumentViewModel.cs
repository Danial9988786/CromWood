namespace CromWood.Business.Models.ViewModel
{
    public class UnitUtilityDocumentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocUrl { get; set; }
        public Guid UnitUtilityId { get; set; }
        public UnitUtilityViewModel UnitUtility { get; set; }
    }
}
