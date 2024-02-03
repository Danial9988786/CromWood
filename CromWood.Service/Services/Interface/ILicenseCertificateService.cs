using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Helper;

namespace CromWood.Business.Services.Interface
{
    public interface ILicenseCertificateService
    {
        public Task<AppResponse<IEnumerable<LicenseCertificateViewModel>>> GetAllLicenseCertificatesList(Guid filterId = default(Guid));
        public Task<AppResponse<LicenseCertificateViewModel>> GetLicenseCertificateView(Guid Id);
        public Task<AppResponse<LicenseCertificateModel>> GetLicenseCertificateById(Guid Id);
        public Task<AppResponse<IEnumerable<LicenseCertificateViewModel>>> GetAllLicenseCertificates(Guid propertyId);
        public Task<AppResponse<int>> AddModifyLicense(LicenseCertificateModel license);
        public Task<AppResponse<int>> DeleteLicense(Guid Id);
        public Task<AppResponse<int>> ArchieveLicense(Guid Id);
        public Task<ExportFile> DownloadLicense(string url);
    }
}
