namespace CromWood.Business.Models.ViewModel
{
    public class UnitUtilityReadingViewModel
    {
        public Guid Id { get; set; }
        public string MeterReading { get; set; }
        public DateTime DateOfReading { get; set; }
        public string Note { get; set; }
        public Guid UnitUtilityId { get; set; }
    }
}
