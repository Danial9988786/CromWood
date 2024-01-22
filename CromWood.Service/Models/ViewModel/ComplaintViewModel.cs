using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class ComplaintViewModel: DBTable
    {
        public Guid Id { get; set; }
        public PropertyViewModel Property { get; set; }
        public ComplaintType ComplaintType { get; set; }
        public TenantViewModel Tenant { get; set; }
        public DateTime? ExpectedResolveDate { get; set; }
        public ComplaintCategory ComplaintCategory { get; set; }
        public ComplaintNature ComplaintNature { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public string StatusUpdateRemark { get; set; }
        public string StatusUpdateFileUrl { get; set; }
    }
}
