using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class Complaint: DBTable
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
        public ComplaintCategory ComplaintCategory { get; set; }
        public Guid ComplaintNatureId { get; set; }
        public ComplaintNature ComplaintNature { get; set; }
        [MaxLength(25)]
        public string Status { get;set; }
        [MaxLength(100)]
        public string FileUrl { get; set; }
    }
}
