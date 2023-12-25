using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using CromWood.Constants;
using CromWood.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    [Authorize]
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IAuthService _authService;
        public AssetController(IAssetService assetService, IAuthService authService)
        {
            _assetService = assetService;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.AssetManagement, PermissionConstant.ViewAll);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _assetService.GetAssetsForList();
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddAsset(Guid Id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.AssetManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            AssetModel? asset;
            if (Id == Guid.Empty)
            {
                asset = new AssetModel() { AssetId = "ASSET-" + RandomAlphaNumbericGenerator.Random(6) };
            }
            else
            {
                var result = await _assetService.GetAssetsOverView(Id);
                asset = result.Data;
            }

            return PartialView("AddAsset", asset);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetModel asset)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.AssetManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            if(asset.Id == Guid.Empty)
            {
                asset.Id = Guid.NewGuid();
                await _assetService.AddAsset(asset);
            }
            else
            {
                await _assetService.EditAsset(asset);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AssetDetail(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.AssetManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _assetService.GetAssetsOverView(id);
            return View(result.Data);
        }
    }
}
