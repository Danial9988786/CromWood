namespace CromWood.Data.Entities
{
    public class PropertyInsurance
    {
        public Guid Id { get; set; }
        public string Insurer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FileUrl { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

    }
}
