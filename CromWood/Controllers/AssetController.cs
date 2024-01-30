using ClosedXML.Excel;
using CromWood.Business.Constants;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
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

        public async Task<IActionResult> Index(Guid filterId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.AssetManagement, PermissionConstant.ViewAll);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _assetService.GetAssetsForList(filterId);
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Export()
        {
            // Get assets for exporting
            var assets = await _assetService.GetAssetsForExport(Guid.Empty);
            var source = assets.Data.ToList();
            List<string> headers = new()
            {"Asset Id", "Asset Type", "House No Street",
 "Locality", "Borough", "Post Code", "Title Number", "Ownership",
 "Aquisition Date", "Purchase Price", "Valuation", "Valuation Date",
 "Reinstatement", "Lender", "Chargee", "Date Of Charge",
 "Financial Prgoram", "Grant Provider", "Attributable Grant", "Construction Period",
 "Landlord Responsible", "Freeholder Responsible", "Owner Responsible", "Landlord Name",
 "Managing Agent", "Managing Agent House No Street", "Managing Agent Locality", "Managing Agent Borough",
 "Managing Agent Post Code", "Lease Term", "Lease Expiry" };

            #region Exporting for Excel

            // This is the best way to export, as we have to loop anyway in another helper as well.
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"asset-lists.xlsx";
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
                worksheet.Cell(data + 1, 2).Value = source[data - 1].AssetType.Name;
                worksheet.Cell(data + 1, 3).Value = source[data - 1].HouseNoStreet;
                worksheet.Cell(data + 1, 4).Value = source[data - 1].Locality;
                worksheet.Cell(data + 1, 5).Value = source[data - 1].Borough;
                worksheet.Cell(data + 1, 6).Value = source[data - 1].PostCode;
                worksheet.Cell(data + 1, 7).Value = source[data - 1].TitleNumber;
                worksheet.Cell(data + 1, 8).Value = source[data - 1].Ownership;
                worksheet.Cell(data + 1, 9).Value = source[data - 1].AquisitionDate;
                worksheet.Cell(data + 1, 10).Value = source[data - 1].PurchasePrice;
                worksheet.Cell(data + 1, 11).Value = source[data - 1].Valuation;
                worksheet.Cell(data + 1, 12).Value = source[data - 1].ValuationDate;
                worksheet.Cell(data + 1, 13).Value = source[data - 1].Reinstatement;
                worksheet.Cell(data + 1, 14).Value = source[data - 1].Lender;
                worksheet.Cell(data + 1, 15).Value = source[data - 1].Chargee;
                worksheet.Cell(data + 1, 16).Value = source[data - 1].DateOfCharge;
                worksheet.Cell(data + 1, 17).Value = source[data - 1].FinancialPrgoram.Name;
                worksheet.Cell(data + 1, 18).Value = source[data - 1].GrantProvider;
                worksheet.Cell(data + 1, 19).Value = source[data - 1].AttributableGrant;
                worksheet.Cell(data + 1, 20).Value = source[data - 1].ConstructionPeriod;
                worksheet.Cell(data + 1, 21).Value = source[data - 1].LandlordResponsible;
                worksheet.Cell(data + 1, 22).Value = source[data - 1].FreeholderResponsible;
                worksheet.Cell(data + 1, 23).Value = source[data - 1].OwnerResponsible;
                worksheet.Cell(data + 1, 24).Value = source[data - 1].LandlordName;
                worksheet.Cell(data + 1, 25).Value = source[data - 1].ManagingAgent;
                worksheet.Cell(data + 1, 26).Value = source[data - 1].ManagingAgentHouseNoStreet;
                worksheet.Cell(data + 1, 27).Value = source[data - 1].ManagingAgentLocality;
                worksheet.Cell(data + 1, 28).Value = source[data - 1].ManagingAgentBorough;
                worksheet.Cell(data + 1, 29).Value = source[data - 1].ManagingAgentPostCode;
                worksheet.Cell(data + 1, 30).Value = source[data - 1].LeaseTerm;
                worksheet.Cell(data + 1, 31).Value = source[data - 1].LeaseExpiry;

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
