﻿using ClosedXML.Excel;
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
        private readonly IChangeLogService _changeLogService;
        private readonly IAuthService _authService;
        private IFileUploader _fileUploader;
        public PropertyController(IPropertyService propertyService, IAmenityService amenityService, ILicenseCertificateService licenseCertificateService, IAuthService authService, IFileUploader fileUploader, IChangeLogService changeLogService)
        {
            _propertyService = propertyService;
            _amenityService = amenityService;
            _licenseCertificateService = licenseCertificateService;
            _authService = authService;
            _fileUploader = fileUploader;
            _changeLogService = changeLogService;
        }
        public async Task<IActionResult> Index(Guid filterId, Guid assetId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.ViewAll);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.GetPropertyForList(filterId);
            // This is provided incase of new addition of Asset to Redirect to Property Addition
            ViewBag.AssetId = assetId;
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Export()
        {
            // Get assets for exporting
            var assets = await _propertyService.GetPropertyForList();
            var source = assets.Data.ToList();
            List<string> headers = new() {
             "Property ID", "Asset ID", "Address", "Expected Monthly Rate",
             "Property Type", "Square Footage", "Floor Number",
             "No Of Bedroom", "No Of Bathroom"}
;

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
            for (int i = 1; i < source.Count; i++)
            {
                worksheet.Cell(i + 1, 1).Value = source[i - 1].PropertyCode;
                worksheet.Cell(i + 1, 2).Value = source[i - 1].Asset.AssetId;
                worksheet.Cell(i + 1, 3).Value = source[i - 1].Asset.StreetAddress;
                worksheet.Cell(i + 1, 4).Value = source[i - 1].Asset.StreetAddress;
                worksheet.Cell(i + 1, 5).Value = source[i - 1].ExpectedMonthlyRate + " monthly";
                worksheet.Cell(i + 1, 6).Value = source[i - 1].PropertyType.Name;
                worksheet.Cell(i + 1, 7).Value = source[i - 1].SquareFootage;
                worksheet.Cell(i + 1, 8).Value = source[i - 1].FloorNumber;
                worksheet.Cell(i + 1, 9).Value = source[i - 1].NoOfBedroom;
                worksheet.Cell(i + 1, 10).Value = source[i - 1].NoOfBathroom;
                worksheet.Cell(i + 1, 11).Value = source[i - 1].Tenancies.Count;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
            #endregion

        }

        [HttpGet]
        public async Task<IActionResult> AddModifyProperty(Guid id, Guid assetId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            if (id != Guid.Empty)
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
            property.AssetId = assetId != Guid.Empty ? assetId : Guid.Empty;
            return PartialView("AddModifyProperty", property);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyPropertyPost(PropertyModel property, bool addTenancy)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _propertyService.AddModifyProperty(property);
            if (addTenancy)
            {
                // Redirect to Tenancy Add Page//
                return RedirectToAction("Index", "Tenancy", new { propertyId = result.Data });
            }
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
            var result = await _propertyService.GetPropertyOverViewPageDetail(id);
            result.Data.Id = id;
            return View("Overview", result.Data);
        }

        public async Task<IActionResult> OverviewJSON(Guid id)
        {
            var result = await _propertyService.GetPropertyOverViewPageJSON(id);
            return StatusCode(result.StatusCode, result.Data);
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

        public async Task<IActionResult> ChangeLog(Guid id, [FromQuery] string propertyId)
        {
            ViewBag.BreadcrumbName = propertyId;
            ViewBag.Id = id;
            var result = await _changeLogService.GetChangeLogsForScreenDetail(id);
            return View(result.Data);
        }
    }
}
