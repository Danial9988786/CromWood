using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class PropertyAssesmentModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Inspection ID")]
        public string InspectionID { get; set; }
        public Guid PropertyId { get; set; }
        [Display(Name = "Date Time")]
        public DateTime? DateTime { get; set; }
        [Display(Name = "Inspector Name")]
        public string InspectorName { get; set; }
        [Display(Name = "Inspector Note")]
        public string InspectionNote { get; set; }
        [Display(Name = "Building Date")]
        public string BuildingDate { get; set; }
        [Display(Name = "Building Note")]
        public string BuildingNote { get; set; }
    }
}
