using CromWood.Data.Entities.Default;
using System.ComponentModel.DataAnnotations;

namespace CromWood.Data.Entities
{
    public class Tenant: DBTable
    {
        public Guid Id { get; set; }
        public Guid SalutationId { get; set; }
        public Salution Salutation { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [MaxLength(100)]
        public string NIN { get; set; }

        // Tenant address
        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [MaxLength(100)]
        public string StreetArea { get; set; }

        [MaxLength(100)]
        public string Landmark { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string County { get; set; }

        [MaxLength(10)]
        public string PostCode { get; set; }
        public Guid? CountryId { get; set; }
        public Country Country { get; set; }

        // Tenant bank details

        [MaxLength(100)]
        public string AccountName { get; set; }

        [MaxLength(100)]
        public string AccountNumber { get; set; }

        [MaxLength(50)]
        public string SortCode { get; set; }

        [MaxLength(100)]
        public string BankName { get; set; }

        public ICollection<TenancyTenant> TenancyTenants { get; set; }
    }
}
