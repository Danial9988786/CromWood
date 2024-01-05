using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class LicenseCertificationController : Controller
    {
        private readonly ILicenseCertificateService _licenseCertificateService;
        public LicenseCertificationController(ILicenseCertificateService licenseCertificateService)
        {
            _licenseCertificateService = licenseCertificateService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _licenseCertificateService.GetAllLicenseCertificates();
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ViewLicense(Guid id)
        {
            var license = await _licenseCertificateService.GetLicenseCertificateView(id);
            return PartialView(license.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyLicense(Guid id)
        {
            var license =  await _licenseCertificateService.GetLicenseCertificateById(id);
            return PartialView(license.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyLicense([FromForm]LicenseCertificateModel license, Guid propertyId)
        {
            await _licenseCertificateService.AddModifyLicense(license);
            if (propertyId != Guid.Empty)
            {
                return RedirectToAction("Licensing", "Property", new { id = propertyId });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ArchieveLicense(Guid id)
        {
            var result = await _licenseCertificateService.ArchieveLicense(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLicense(Guid id)
        {
            var result = await _licenseCertificateService.DeleteLicense(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Download(string url) {
            var result = await _licenseCertificateService.DownloadLicense(url);
            return File(result.bytes,result.ContentType, result.Name);
        }
    }
}
