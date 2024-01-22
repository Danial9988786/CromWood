namespace CromWood.Business.Models
{
    public class PropertyAssesmentModel
    {
        public Guid Id { get; set; }
        public string InspectionID { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime? DateTime { get; set; }
        public string InspectorName { get; set; }
        public string InspectionNote { get; set; }
        public string BuildingDate { get; set; }
        public string BuildingNote { get; set; }
    }
}
