using CromWood.Data.Entities;

namespace CromWood.Business.Models
{
    public class UnitUtilityReadingModel
    {
        public Guid Id { get; set; }
        public string MeterReading { get; set; }
        public DateTime DateOfReading { get; set; }
        public string Note { get; set; }
        public Guid UnitUtilityId { get; set; }
    }
}
