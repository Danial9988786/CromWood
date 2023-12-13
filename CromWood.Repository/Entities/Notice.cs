using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class Notice
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string TrackingNumber { get; set; }
        public Guid ConcernId { get; set; }
        public Concern Concern { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string ASTLicense { get; set; }
        public Guid SectionId { get; set; }
        public Section Section { get; set; }
        public bool IsDrafted { get; set; }
        public DateTime EmailedOn { get; set; }
        public DateTime ServedAndPictured { get; set; }
        public DateTime ExpiryDate { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

    }
}
