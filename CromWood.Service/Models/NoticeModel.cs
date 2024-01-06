using CromWood.Data.Entities.Default;
using CromWood.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class NoticeModel
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string TrackingNumber { get; set; }
        public Guid ConcernId { get; set; }
        public Guid TenantId { get; set; }
        public string ASTLicense { get; set; }
        public Guid SectionId { get; set; }
        public bool IsDrafted { get; set; }
        public bool Archived { get; set; }
        public DateTime? EmailedOn { get; set; }
        public DateTime? ServedAndPictured { get; set; }
        public DateTime? ExpiryDate { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }
    }
}
