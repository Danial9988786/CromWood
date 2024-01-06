using CromWood.Data.Entities.Default;

namespace CromWood.Business.Models.ViewModel
{
    public class ClaimViewModel
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public TenantViewModel Tenant { get; set; }
        public string ClaimReference { get; set; }
        public Guid? ClaimTypeId { get; set; }
        public ClaimType ClaimType { get; set; }
        public string ClaimNo { get; set; }
        public float? Fee { get; set; }
        public DateTime? HearingDate { get; set; }
        public Guid? CourtId { get; set; }
        public Court Court { get; set; }
        public string Comment { get; set; }
        public string Action { get; set; }
    }
}
