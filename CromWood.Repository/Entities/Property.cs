using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class Property: DBTable
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string PropertyCode { get; set; }
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; }
        public float ExpectedMonthlyRate { get; set; }

        // Structural details
        public Guid PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        [MaxLength(25)]
        public string SquareFootage { get; set; }

        [MaxLength(25)]
        public string FloorNumber { get; set; }
        public float NoOfBedroom { get; set; }
        public float NoOfBathroom { get; set; }
        public ICollection<PropertyAmenity> PropertyAmenities { get; set; }
        public ICollection<Tenancy> Tenancies { get; set; }
        public PropertyInsurance PropertyInsurance { get; set; }

    }
}
