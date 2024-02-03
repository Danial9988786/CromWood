using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class LicenseCertificateModel
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        [Display(Name = "License Certificate Type")]
        public Guid LicenseCertificateTypeId { get; set; }
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        [MaxLength(200)]
        public string DocUrl { get; set; }
        public IFormFile DocFile { get; set; }
        [MaxLength(500)]
        [Display(Name = "Note")]
        public string Note { get; set; }
        public bool? Archieved { get; set; }
    }
}
