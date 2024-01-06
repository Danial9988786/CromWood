using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class ClaimModel
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        [MaxLength(100)]
        public string ClaimReference { get; set; }
        public Guid? ClaimTypeId { get; set; }

        [MaxLength(25)]
        public string ClaimNo { get; set; }
        public float? Fee { get; set; }
        public DateTime? HearingDate { get; set; }
        public Guid? CourtId { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        [MaxLength(500)]
        public string Action { get; set; }
    }
}
