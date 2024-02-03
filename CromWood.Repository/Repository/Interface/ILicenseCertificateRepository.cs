using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface ILicenseCertificateRepository
    {
        public Task<IEnumerable<LicenseCertificate>> GetAllLicenseCertificatesList(Guid filterId);
        public Task<IEnumerable<LicenseCertificate>> GetAllLicenseCertificates(Guid propertyId);
        public Task<LicenseCertificate> GetLicenseCertificateById(Guid Id);
        public Task<int> AddLicense(LicenseCertificate license);
        public Task<int> EditLicense(LicenseCertificate license);
        public Task<string> DeleteLicense(Guid Id);
        public Task<int> ArchieveLicense(Guid Id);
    }
}
