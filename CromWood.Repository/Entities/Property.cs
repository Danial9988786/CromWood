namespace CromWood.Data.Entities
{
    public class Property
    {
        public Guid Id { get; set; }
        public string PropertyCode { get; set; }
        public Guid AssetId { get; set; }
        public float ExpectedMonthlyRate { get; set; }

        // Structural details
        public string PropertyType { get; set; }
        public string SquareFootage { get; set; }
        public string FloorNumber { get; set; }
        public float NoOfBedroom { get; set; }
        public float NoOfBathroom { get; set; }
        public List<string> Amenities {  get; set; }
    }
}
