namespace CromWood.Data.Entities
{
    public class UnitUtilityReading : DBTable
    {
        public Guid Id { get; set; }
        public string MeterReading { get; set; }
        public DateTime DateOfReading { get; set; }
        public string Note { get; set; }
        public Guid UnitUtilityId { get; set; }
        public UnitUtility UnitUtility { get; set; }
    }
}
