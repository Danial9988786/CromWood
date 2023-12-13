using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Context
{
    public class CromwoodContext : DbContext
    {
        public CromwoodContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Test> Tests { get; set; }

        #region Default tables
        public DbSet<ClaimType> ClaimTypes { get; set; }
        public DbSet<ComplaintCategory> ComplaintCategories { get; set; }
        public DbSet<ComplaintNature> ComplaintNatures { get; set; }
        public DbSet<ComplaintType> ComplaintTypes { get; set; }
        public DbSet<Concern> Concerns { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<FinancialPrgoram> FinancialPrgorams { get; set; }
        public DbSet<LicenseCertificateType> LicenseCertificateTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<RentFrequency> RentFrequencies { get; set; }
        public DbSet<Salution> Salutions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<TenantType> TenantTypes { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        #endregion

        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenancy> Tenancies { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<LicenseCertificate> LicenseCertificates { get; set; }
        public DbSet<Notice> Notice { get; set; }

    }
}
