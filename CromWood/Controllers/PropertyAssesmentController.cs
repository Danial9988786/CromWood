using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Helper;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class PropertyAssesmentController : Controller
    {
        private readonly IPropertyAssesmentService _assesmentService;
        public PropertyAssesmentController(IPropertyAssesmentService assesmentService)
        {
            _assesmentService = assesmentService;
        }

        public async Task<IActionResult> Index(Guid filterId)
        {
            var result = await _assesmentService.GetPropertyAssesments(filterId);
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AddModifyPropertyAssesment()
        {
            var model = new PropertyAssesmentModel()
            {
                InspectionID = "ASM-" + RandomAlphaNumbericGenerator.Random(6),
            };
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyPropertyAssesment(PropertyAssesmentModel assesmentModel)
        {
            await _assesmentService.AddModifyPropertyAssesment(assesmentModel);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuildingDetail(Guid id)
        {
            var result = await _assesmentService.GetPropertyAssesment(id);
            return View(result.Data);
        }

        public async Task<IActionResult> InspectionDetail(Guid id)
        {
            var result = await _assesmentService.GetPropertyAssesment(id);
            return View(result.Data);
        }

        #region Building Items related things

        public async Task<IActionResult> BuildingItems(Guid id)
        {
            var result = await _assesmentService.GetInspectionItems(id);
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AddModifyPropertyAssesmentItem(Guid assesmentId)
        {
            var result = new PropertyInspectionItemModel() {  PropertyAssesmentId = assesmentId, ConditionRating =0};
            return PartialView(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyPropertyAssesmentItem([FromForm] PropertyInspectionItemModel assesmentModel)
        {
            var result = await _assesmentService.AddModifyPropertyAssesmentItem(assesmentModel);
            return RedirectToAction("BuildingItems", new { id= assesmentModel.PropertyAssesmentId});
        }

        public async Task<IActionResult> BuildingImages(Guid id)
        {
            var result = await _assesmentService.GetBuildingImages(id);
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AddModifyPropertyAssesmentImage(Guid assesmentId)
        {
            var result = new PropertyInspectionItemImageModel() { PropertyAssesmentId = assesmentId };
            return PartialView(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyPropertyAssesmentImage([FromForm] PropertyInspectionItemImageModel imageModel)
        {
            var result = await _assesmentService.AddModifyPropertyAssesmentImage(imageModel);
            return RedirectToAction("BuildingImages", new { id = imageModel.PropertyAssesmentId });
        }
        #endregion
    }
}
