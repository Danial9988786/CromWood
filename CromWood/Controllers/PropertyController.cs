using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CromWood.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        private IPropertyService _propertyService;
        private IAmenityService _amenityService;
        public PropertyController(IPropertyService propertyService, IAmenityService amenityService)
        {
            _propertyService = propertyService;
            _amenityService = amenityService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _propertyService.GetPropertyForList();
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddProperty()
        {
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
            property.Id = Guid.NewGuid();
            await _propertyService.AddProperty(property);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await _propertyService.GetPropertyOverView(id);
            return View(result.Data);
        }

        public async Task<IActionResult> Overview(Guid id)
        {
            var result = await _propertyService.GetPropertyOverView(id);
            return View("Overview");
        }

        #region Insurance related operations
        public async Task<IActionResult> Insurance(Guid id)
        {
            var result = await _propertyService.GetPropertyInsuranceDetail(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyInsurance(Guid propertyId)
        {
            var insurance = await _propertyService.GetPropertyInsuranceDetail(propertyId);
            return PartialView(insurance.Data ?? new PropertyInsuranceModel() { PropertyId = propertyId });
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyInsurance(PropertyInsuranceModel insurance)
        {
            await _propertyService.AddModifyInsurance(insurance);
            return RedirectToAction("Insurance", new { id = insurance.PropertyId });
        }
        #endregion

        #region Keys related operations
        public async Task<IActionResult> Keys(Guid id)
        {
            var result = await _propertyService.GetPropertyKeys(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyKey(Guid propertyId, Guid keyId)
        {
            if(keyId!= Guid.Empty)
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
        public async Task<IActionResult> AddModifyKey(PropertyKeyModel key)
        {
            var result = await _propertyService.AddModifyKey(key);
            return RedirectToAction("Keys", new { id = key.PropertyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteKey(Guid propertyId, Guid keyId)
        {
            var result = await _propertyService.DeleteKey(keyId);
            return RedirectToAction("Keys", new { id = propertyId });
        }
        #endregion
    }
}
