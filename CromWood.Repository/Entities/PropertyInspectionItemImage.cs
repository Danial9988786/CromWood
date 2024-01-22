namespace CromWood.Data.Entities
{
    // This is the table containing image from PropertyInspectionItem or PropertyAssesment only
    public class PropertyInspectionItemImage
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Guid? PropertyInspectionItemId { get; set; }
        public PropertyInspectionItem PropertyInspectionItem { get; set; }
        public Guid? PropertyAssesmentId { get; set; }
        public PropertyAssesment PropertyAssesment { get; set; }
    }
}
