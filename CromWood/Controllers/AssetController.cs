using ClosedXML.Excel;
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

        [HttpPost]
        public async Task<IActionResult> Export()
        {
            // Get assets for exporting
            var assets = await _assetService.GetAssetsForList();
            var source = assets.Data.ToList();
            List<string> headers = new() { "Asset ID", "Street Address", "Ownership", "Availble Units" };

            #region Exporting for Excel

            // This is the best way to export, as we have to loop anyway in another helper as well.
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"asset-management.xlsx";
            string tabName = "Assets";

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
                worksheet.Cell(data + 1, 1).Value = source[data - 1].AssetId;
                worksheet.Cell(data + 1, 2).Value = source[data - 1].StreetAddress;
                worksheet.Cell(data + 1, 3).Value = source[data - 1].Ownership;
                worksheet.Cell(data + 1, 4).Value = "0/2";
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
            #endregion
        }

        [HttpGet]
        public async Task<IActionResult> AddAsset(Guid Id, string section)
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
                // For edit by section
                if (!string.IsNullOrEmpty(section))
                {
                    result.Data.Section = section;
                }
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
            if (asset.Id == Guid.Empty)
            {
                asset.Id = Guid.NewGuid();
                await _assetService.AddAsset(asset);
            }
            else
            {
                await _assetService.EditAsset(asset);
                return RedirectToAction("AssetDetail", "Asset", new { id = asset.Id });
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

        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await _assetService.GetAssetsOverView(id);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
