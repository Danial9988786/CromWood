using CromWood.Data.Entities;
using CromWood.Data.Entities.Default;
using CromWood.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CromWood.Data.Context
{

    public interface IEntity
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }

    public class CromwoodContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Guid UserId { get; set; }

        public CromwoodContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            if (_httpContextAccessor?.HttpContext != null)
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                    UserId = IdentityExtension.GetId(_httpContextAccessor.HttpContext.User);
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
        public DbSet<PropertyKeyType> PropertyKeyTypes { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
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
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyKey> PropertyKeys { get; set; }
        public DbSet<PropertyInsurance> PropertyInsurances { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public DbSet<Tenancy> Tenancies { get; set; }
        public DbSet<TenancyNote> TenancyNotes { get; set; }
        public DbSet<TenancyDocument> TenancyDocuments { get; set; }
        public DbSet<TenancyTenant> TenancyTenants { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintComment> ComplaintComments { get; set; }
        public DbSet<LicenseCertificate> LicenseCertificates { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageRecipient> MessageRecipients { get; set; }

        // Related to Property Assesment.
        public DbSet<DetailItem> DetailItems { get; set; }
        public DbSet<SurverySection> SurverySections { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public DbSet<PropertyAssesment> PropertyAssesments { get; set; }
        public DbSet<PropertyInspectionItem> PropertyInspectionItems { get; set; }
        public DbSet<PropertyInspectionItemImage> PropertyInspectionItemImage { get; set; }


        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void OnBeforeSaving()
        {
            var now = DateTime.UtcNow;

            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is DBTable entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = now;
                            entity.UpdatedDate = now;
                            entity.CreatedBy = UserId;
                            entity.UpdatedBy = UserId;
                            break;
                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                            entity.UpdatedDate = now;
                            entity.UpdatedBy = UserId;
                            break;
                    }
                }
            }
        }

    }
}
