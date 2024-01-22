using ClosedXML.Excel;
using CromWood.Business.Constants;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        private IPropertyService _propertyService;
        private IAmenityService _amenityService;
        private ILicenseCertificateService _licenseCertificateService;
        private readonly IAuthService _authService;
        private IFileUploader _fileUploader;
        public PropertyController(IPropertyService propertyService, IAmenityService amenityService, ILicenseCertificateService licenseCertificateService, IAuthService authService, IFileUploader fileUploader)
        {
            _propertyService = propertyService;
            _amenityService = amenityService;
            _licenseCertificateService = licenseCertificateService;
            _authService = authService;
            _fileUploader = fileUploader;
        }
        public async Task<IActionResult> Index()
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.ViewAll);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.GetPropertyForList();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Export()
        {
            // Get assets for exporting
            var assets = await _propertyService.GetPropertyForList();
            var source = assets.Data.ToList();
            List<string> headers = new() { "Property ID", "Street Address", "Ownership", "Status", "Rent Amount" };

            #region Exporting for Excel

            // This is the best way to export, as we have to loop anyway in another helper as well.
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"property-management.xlsx";
            string tabName = "Properties";

            using var workbook = new XLWorkbook();
            IXLWorksheet worksheet =
            workbook.Worksheets.Add(tabName);

            // For Header
            for (int header = 1; header <= headers.Count; header++)
            {
                worksheet.Cell(1, header).Value = headers[header - 1];
                worksheet.Cell(1, header).Style.Font.SetBold();
            }

            // For Data
            for (int data = 1; data <= source.Count; data++)
            {
                worksheet.Cell(data + 1, 1).Value = source[data - 1].PropertyCode;
                worksheet.Cell(data + 1, 2).Value = source[data - 1].Asset.StreetAddress;
                worksheet.Cell(data + 1, 3).Value = source[data - 1].Asset.Ownership;
                worksheet.Cell(data + 1, 4).Value = "Available to let";
                worksheet.Cell(data + 1, 5).Value = source[data - 1].ExpectedMonthlyRate + " monthly";
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
            #endregion

        }

        [HttpGet]
        public async Task<IActionResult> AddModifyProperty(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            if(id != Guid.Empty)
            {
                var result = await _propertyService.GetPropertyOverView(id);
                return PartialView("AddModifyProperty", result.Data);
            }
            var property = new PropertyModel();
            var amenities = await _amenityService.GetAmenities();
            var propertyAmenities = new List<PropertyAmenity>();
            foreach (var amenity in amenities.Data)
            {
                propertyAmenities.Add(new PropertyAmenity()
                {
                    Amenity = amenity,
                });
            }
            property.PropertyAmenities = propertyAmenities;
            property.PropertyCode = "PROP-" + RandomAlphaNumbericGenerator.Random(6);
            return PartialView("AddModifyProperty", property);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyProperty(PropertyModel property)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            await _propertyService.AddModifyProperty(property);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.GetPropertyOverView(id);
            return View(result.Data);
        }

        public async Task<IActionResult> Overview(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            await _propertyService.GetPropertyOverView(id);
            return View("Overview");
        }

        #region Insurance related operations
        public async Task<IActionResult> Insurance(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.GetPropertyInsuranceDetail(id);
            return View(result.Data);
        }

        
        [HttpPost]
        public async Task<IActionResult> DownloadInsurance(string url)
        {
            var result = await _fileUploader.Download(url, "propertyinsurance");
            return File(result.bytes, result.ContentType, result.Name);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyInsurance(Guid propertyId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var insurance = await _propertyService.GetPropertyInsuranceDetail(propertyId);
            return PartialView(insurance.Data ?? new PropertyInsuranceModel() { PropertyId = propertyId });
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyInsurance([FromForm] PropertyInsuranceModel insurance)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            await _propertyService.AddModifyInsurance(insurance);
            return RedirectToAction("Insurance", new { id = insurance.PropertyId });
        }
        #endregion

        #region Keys related operations
        public async Task<IActionResult> Keys(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.GetPropertyKeys(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyKey(Guid propertyId, Guid keyId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            if (keyId != Guid.Empty)
            {
                var result = await _propertyService.GetPropertyKey(keyId);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new PropertyKeyModel() { PropertyId = propertyId, SharedWithTenant = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyKey([FromForm] PropertyKeyModel key)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.AddModifyKey(key);
            return RedirectToAction("Keys", new { id = key.PropertyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteKey(Guid keyId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanDelete);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.DeleteKey(keyId);
            return StatusCode(result.StatusCode, result.Data);
        }
        #endregion

        public async Task<IActionResult> Tenancy(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.GetPropertyTenancy(id);
            return View(result.Data);
        }

        #region License & Certification
        public async Task<IActionResult> Licensing(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _licenseCertificateService.GetAllLicenseCertificates(id);
            return View(result.Data);
        }
        #endregion
    }
}
