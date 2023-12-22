using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class LicenseCertificate: DBTable
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        public Guid LicenseCertificateTypeId { get; set; }
        public LicenseCertificateType LicenseCertificateType { get; set; }
        public DateTime ExpiryDate { get; set; }

        [MaxLength(200)]
        public string DocUrl { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }
        public bool? Archieved { get; set; }
    }
}
