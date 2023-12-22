using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;

namespace CromWood.Business.Services.Implementation
{
    public class LicenseCertificateService : ILicenseCertificateService
    {
        private readonly ILicenseCertificateRepository _licenseRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploader _fileUploader;
        public LicenseCertificateService(ILicenseCertificateRepository licenseRepository, IMapper mapper, IFileUploader uploader)
        {
            _licenseRepository = licenseRepository;
            _mapper = mapper;
            _fileUploader = uploader;
        }

        public async Task<AppResponse<IEnumerable<LicenseCertificateViewModel>>> GetAllLicenseCertificates()
        {
            try
            {
                var result = await _licenseRepository.GetAllLicenseCertificates();
                var mappedResult = _mapper.Map<IEnumerable<LicenseCertificateViewModel>>(result);
                return ResponseCreater<IEnumerable<LicenseCertificateViewModel>>.CreateSuccessResponse(mappedResult, "licenses loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<LicenseCertificateViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<LicenseCertificateViewModel>> GetLicenseCertificateView(Guid Id)
        {
            try
            {
                LicenseCertificate result = await _licenseRepository.GetLicenseCertificateById(Id);
                LicenseCertificateViewModel mappedResult = _mapper.Map<LicenseCertificateViewModel>(result);
                return ResponseCreater<LicenseCertificateViewModel>.CreateSuccessResponse(mappedResult, "license loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<LicenseCertificateViewModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<LicenseCertificateModel>> GetLicenseCertificateById(Guid Id)
        {
            try
            {
                LicenseCertificate result = await _licenseRepository.GetLicenseCertificateById(Id);
                LicenseCertificateModel mappedResult = _mapper.Map<LicenseCertificateModel>(result);
                return ResponseCreater<LicenseCertificateModel>.CreateSuccessResponse(mappedResult, "license loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<LicenseCertificateModel>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<IEnumerable<LicenseCertificateViewModel>>> GetAllLicenseCertificates(Guid propertyId)
        {
            try
            {
                var result = await _licenseRepository.GetAllLicenseCertificates(propertyId);
                var mappedResult = _mapper.Map<IEnumerable<LicenseCertificateViewModel>>(result);
                return ResponseCreater<IEnumerable<LicenseCertificateViewModel>>.CreateSuccessResponse(mappedResult, "licenses loaded successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<LicenseCertificateViewModel>>.CreateErrorResponse(null, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> AddModifyLicense(LicenseCertificateModel license)
        {
            try
            {
                LicenseCertificate mappedRequest = _mapper.Map<LicenseCertificate>(license);
                int result = 0;

                if (license.DocFile != null)
                {
                    // In case of Edit, delete prev file & add new one
                    if (mappedRequest.Id != Guid.Empty)
                    {
                        await _fileUploader.Delete(mappedRequest.DocUrl, "licensecertification");
                    }
                    mappedRequest.DocUrl = await _fileUploader.Upload(license.DocFile, "licensecertification");
                }

                result = mappedRequest.Id == Guid.Empty ? await _licenseRepository.AddLicense(mappedRequest) : await _licenseRepository.EditLicense(mappedRequest);

                return ResponseCreater<int>.CreateSuccessResponse(result, "License add/update successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> ArchieveLicense(Guid Id)
        {
            try
            {
                int result = await _licenseRepository.ArchieveLicense(Id);
                return ResponseCreater<int>.CreateSuccessResponse(result, "License archived successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }

        public async Task<AppResponse<int>> DeleteLicense(Guid Id)
        {
            try
            {
                string docUrl = await _licenseRepository.DeleteLicense(Id);
                if (docUrl != null) await _fileUploader.Delete(docUrl, "licensecertification");
                return ResponseCreater<int>.CreateSuccessResponse(1, "license deleted successfully");
            }

            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.ToString());
            }
        }
    }
}
