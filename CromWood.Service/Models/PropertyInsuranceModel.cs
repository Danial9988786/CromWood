namespace CromWood.Business.Models
{
    public class PropertyInsuranceModel
    {
        public Guid Id { get; set; }
        public string Insurer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FileUrl { get; set; }
        public Guid PropertyId { get; set; }
    }
}
