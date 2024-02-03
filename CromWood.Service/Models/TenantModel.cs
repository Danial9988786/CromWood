using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Business.Models
{
    public class TenantModel
    {
        public Guid Id { get; set; }
        [Display(Name ="Salutation")]
        public Guid SalutationId { get; set; }
        public Salution Salutation { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required, MaxLength(20)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(100)]
        [Display(Name = "NIN")]
        public string NIN { get; set; }

        // Tenant address
        [MaxLength(100)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Street Area")]
        public string StreetArea { get; set; }

        [MaxLength(100)]
        [Display(Name = "Landmark")]
        public string Landmark { get; set; }

        [MaxLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [MaxLength(100)]
        [Display(Name = "County")]
        public string County { get; set; }

        [MaxLength(10)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Display(Name = "Country")]
        public Guid? CountryId { get; set; }
        public Country Country { get; set; }

        // Tenant bank details

        [MaxLength(100)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [MaxLength(50)]
        [Display(Name = "Sort Code")]
        public string SortCode { get; set; }

        [MaxLength(100)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
    }
}
