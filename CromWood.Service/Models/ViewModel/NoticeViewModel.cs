using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class NoticeViewModel: DBTable
    {
        public Guid Id { get; set; }
        public string TrackingNumber { get; set; }
        public Guid ConcernId { get; set; }
        public Concern Concern { get; set; }
        public Guid TenantId { get; set; }
        public TenantViewModel Tenant { get; set; }
        public string ASTLicense { get; set; }
        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public bool IsDrafted { get; set; }
        public bool Archived { get; set; }
        public DateTime? EmailedOn { get; set; }
        public DateTime? ServedAndPictured { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Comment { get; set; }
    }
}
