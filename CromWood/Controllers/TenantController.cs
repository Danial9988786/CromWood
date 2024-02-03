using ClosedXML.Excel;
using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    [Authorize]
    public class TenantController : Controller
    {
        private readonly ITenantService _tenantService;
        private readonly IChangeLogService _changeLogService;
        public TenantController(ITenantService tenantService, IChangeLogService changeLogService)
        {
            _tenantService = tenantService;
            _changeLogService = changeLogService;
        }
        public async Task<IActionResult> Index(Guid filterId)
        {
            var result = await _tenantService.GetTenantForList(filterId);
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Export()
        {
            // Get assets for exporting
            var assets = await _tenantService.GetTenantForExport();
            var source = assets.Data.ToList();
            List<string> headers = new()
            {"Id", "Salutation", "Full Name", "Phone",
             "Email", "NIN", "AddressLine1", "AddressLine2", "StreetArea",
             "Landmark", "City", "County", "Post Code",
             "Country", "Account Name", "Account Number", "Sort Code",
             "Bank Name"};
            ;

            #region Exporting for Excel

            // This is the best way to export, as we have to loop anyway in another helper as well.
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"tenant-lists.xlsx";
            string tabName = "Tenants";

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
                worksheet.Cell(data + 1, 1).Value = source[data - 1].Id.ToString();
                worksheet.Cell(data + 1, 2).Value = source[data - 1].Salutation.Name;
                worksheet.Cell(data + 1, 3).Value = source[data - 1].FullName;
                worksheet.Cell(data + 1, 4).Value = source[data - 1].Phone;
                worksheet.Cell(data + 1, 5).Value = source[data - 1].Email;
                worksheet.Cell(data + 1, 6).Value = source[data - 1].NIN;
                worksheet.Cell(data + 1, 7).Value = source[data - 1].AddressLine1;
                worksheet.Cell(data + 1, 8).Value = source[data - 1].AddressLine2;
                worksheet.Cell(data + 1, 9).Value = source[data - 1].StreetArea;
                worksheet.Cell(data + 1, 10).Value = source[data - 1].Landmark;
                worksheet.Cell(data + 1, 11).Value = source[data - 1].City;
                worksheet.Cell(data + 1, 12).Value = source[data - 1].County;
                worksheet.Cell(data + 1, 13).Value = source[data - 1].PostCode;
                worksheet.Cell(data + 1, 14).Value = source[data - 1].Country.Name;
                worksheet.Cell(data + 1, 15).Value = source[data - 1].AccountName;
                worksheet.Cell(data + 1, 16).Value = source[data - 1].AccountNumber;
                worksheet.Cell(data + 1, 17).Value = source[data - 1].SortCode;
                worksheet.Cell(data + 1, 18).Value = source[data - 1].BankName;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
            #endregion
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await _tenantService.GetTenantOverView(id);
            return View(result.Data);
        }

        public async Task<IActionResult> Tenancies(Guid id)
        {
            var result = await _tenantService.GetTenanciesForTenant(id);
            return View(result.Data);
        }

        public async Task<IActionResult> BankDetail(Guid id)
        {
            var result = await _tenantService.GetTenantOverView(id);
            return View(result.Data);
        }

        public async Task<IActionResult> ModifyTenant(Guid id, string from)
        {
            var tenant = await _tenantService.GetTenantOverView(id);
            ViewBag.ForBank = from == "bank";
            return PartialView(tenant.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyTenant(TenantModel tenant)
        {
            await _tenantService.AddModifyTenant(tenant);
            return RedirectToAction("Detail", new { tenant.Id });
        }
        public async Task<IActionResult> ChangeLog(Guid id, [FromQuery] string tenantName)
        {
            ViewBag.BreadcrumbName = tenantName;
            ViewBag.Id = id;
            var result = await _changeLogService.GetChangeLogsForScreenDetail(id);
            return View(result.Data);
        }
    }
}
