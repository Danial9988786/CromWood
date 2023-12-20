using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class PropertyModel
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string PropertyCode { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
        public float ExpectedMonthlyRate { get; set; }
        public Guid PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        [MaxLength(25)]
        public string SquareFootage { get; set; }

        [MaxLength(25)]
        public string FloorNumber { get; set; }
        public float NoOfBedroom { get; set; }
        public float NoOfBathroom { get; set; }
        public List<PropertyAmenity> PropertyAmenities { get; set; }
    }
}
