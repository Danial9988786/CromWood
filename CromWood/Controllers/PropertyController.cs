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
        public PropertyController(IPropertyService propertyService, IAmenityService amenityService, ILicenseCertificateService licenseCertificateService, IAuthService authService)
        {
            _propertyService = propertyService;
            _amenityService = amenityService;
            _licenseCertificateService = licenseCertificateService;
            _authService = authService;
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

        [HttpGet]
        public async Task<IActionResult> AddProperty()
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
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
            return PartialView("AddProperty", property);
        }

        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertyModel property)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            property.Id = Guid.NewGuid();
            await _propertyService.AddProperty(property);
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
                return PartialView(new PropertyKeyModel() { PropertyId = propertyId });
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
