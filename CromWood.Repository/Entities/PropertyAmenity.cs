using CromWood.Data.Entities.Default;

namespace CromWood.Data.Entities
{
    public class PropertyAmenity
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        public Guid AmenityId { get; set; }
        public Amenity Amenity { get; set; }
        public bool Included { get; set; }
    }
}
