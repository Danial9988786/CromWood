using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class Claim
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }

        [MaxLength(100)]
        public string ClaimReference { get; set; }
        public Guid ClaimTypeId { get; set; }
        public ClaimType ClaimType { get; set; }

        [MaxLength(25)]
        public string ClaimNo { get; set; }
        public float Fee { get; set; }
        public DateTime HearingDate { get; set; }
        public Guid CourtId { get; set; }
        public Court Court { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        [MaxLength(500)]
        public string Action { get; set; }
    }
}
