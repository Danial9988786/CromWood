using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Helper;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _assetService.GetAssetsForList();
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult AddAsset()
        {
            return PartialView("AddAsset", new AssetModel() { AssetId = "ASSET-" + RandomAlphaNumbericGenerator.Random(6) });
        }

        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetModel asset)
        {
            asset.Id = Guid.NewGuid();
            await _assetService.AddAsset(asset);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AssetDetail(Guid id) {
            var result= await _assetService.GetAssetsOverView(id);
            return View(result.Data);
        }
    }
}
