using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class LicenseCertificateModel
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid LicenseCertificateTypeId { get; set; }
        public DateTime ExpiryDate { get; set; }
        [MaxLength(200)]
        public string DocUrl { get; set; }
        [MaxLength(500)]
        public string Note { get; set; }
    }
}
