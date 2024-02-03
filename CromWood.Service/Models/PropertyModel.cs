using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class PropertyModel:DBTable
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "Property ID")]
        public string PropertyCode { get; set; }
        public Guid AssetId { get; set; }
        public AssetModel Asset { get; set; }
        [Display(Name = "Expected Monthly Rate")]
        public float ExpectedMonthlyRate { get; set; }
        [Display(Name = "Property Type")]
        public Guid PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        [MaxLength(25)]
        [Display(Name = "Square Footage")]
        public string SquareFootage { get; set; }

        [MaxLength(25)]
        [Display(Name = "Floor Number")]
        public string FloorNumber { get; set; }
        [Display(Name = "No Of Bedroom")]
        public float NoOfBedroom { get; set; }
        [Display(Name = "No Of Bathroom")]
        public float NoOfBathroom { get; set; }
        public List<PropertyAmenity> PropertyAmenities { get; set; }
        public List<Tenancy> Tenancies { get; set; }
    }
}
