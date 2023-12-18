using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models
{
    public class PropertyViewModel
    {
        public Guid Id { get; set; }
        public string PropertyCode { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
        public float ExpectedMonthlyRate { get; set; }
        public Guid PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public string SquareFootage { get; set; }
        public string FloorNumber { get; set; }
        public float NoOfBedroom { get; set; }
        public float NoOfBathroom { get; set; }
        public ICollection<PropertyAmenity> PropertyAmenities { get; set; }
    }
}
