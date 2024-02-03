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

        public async Task<IActionResult> Index(Guid filterId)
        {
            var result = await _licenseCertificateService.GetAllLicenseCertificatesList(filterId);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ViewLicense(Guid id)
        {
            var license = await _licenseCertificateService.GetLicenseCertificateView(id);
            return PartialView(license.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyLicense(Guid id, Guid propId)
        {
            var license =  await _licenseCertificateService.GetLicenseCertificateById(id);
            ViewBag.PropertyId = propId;
            return PartialView(license.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyLicense([FromForm] LicenseCertificateModel license, Guid propId)
        {
            await _licenseCertificateService.AddModifyLicense(license);
            if (propId != Guid.Empty)
            {
                return RedirectToAction("Licensing", "Property", new { id = propId });
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
