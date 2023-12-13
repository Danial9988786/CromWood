using CromWood.Data.Entities.Default;

namespace CromWood.Data.Entities
{
    public class Complaint
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        public Guid ComplaintTypeId { get; set; }
        public ComplaintType ComplaintType { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public DateTime ExpectedResolveDate { get; set; }
        public Guid ComplaintCategoryId { get; set; }
        public ComplaintCategroy ComplaintCategory { get; set; }
        public Guid ComplaintNatureId { get; set; }
        public ComplaintNature ComplaintNature { get; set; }
        public string Status { get;set; }
        public string FileUrl { get; set; }
    }
}
