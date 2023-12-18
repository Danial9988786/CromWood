namespace CromWood.Business.Models
{
    public class AssetViewModel
    {
        public Guid Id { get; set; }
        public string AssetId { get; set; }
        public string StreetAddress { get; set; }
        public string Ownership { get; set; }
        public int AvailableUnit { get; set; }
        public int TotalUnit { get; set; }
    }
}
