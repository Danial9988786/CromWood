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
    public class TenancyController : Controller
    {
        private readonly ITenancyService _tenancyService;
        private readonly IAuthService _authService;

        public TenancyController(ITenancyService tenancyService, IAuthService authService)
        {
            _tenancyService = tenancyService;
            _authService = authService;
        }

        #region TENANCY RELATED CODES
        public async Task<IActionResult> Index()
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.ViewAll);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var tenancies = await _tenancyService.GetTenancyForList();
            return View(tenancies.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Export()
        {
            // Get tenancies for exporting
            var tenancies = await _tenancyService.GetTenancyForList();
            var source = tenancies.Data.ToList();
            List<string> headers = new() { "Tenancy ID", "Property Address", "Property Owner", "Start Date", "End Date", "Status", "Rent" };


            #region Exporting for Excel
            // This is the best way to export, as we have to loop anyway in another helper as well.
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"tenant-management.xlsx";
            string tabName = "Tenancies";


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
                worksheet.Cell(data + 1, 1).Value = source[data - 1].TenancyId;
                worksheet.Cell(data + 1, 2).Value = source[data - 1].Property.Asset.StreetAddress;
                worksheet.Cell(data + 1, 3).Value = source[data - 1].Property.Asset.Ownership;
                worksheet.Cell(data + 1, 4).Value = source[data - 1].StartDate;
                worksheet.Cell(data + 1, 5).Value = source[data - 1].EndDate;
                worksheet.Cell(data + 1, 6).Value = "Rent Eaer";
                worksheet.Cell(data + 1, 7).Value = source[data - 1].RentAmount;
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
            #endregion
        }

        public async Task<IActionResult> AddTenancy()
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var tenancy = new TenancyModel() { TenancyId = "TNT-" + RandomAlphaNumbericGenerator.Random(6), StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1) };
            return PartialView(tenancy);
        }

        [HttpPost]
        public async Task<IActionResult> AddTenancy(TenancyModel tenancy)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            foreach (var tenancyTenant in tenancy.TenancyTenants)
            {
                tenancyTenant.TenantId = tenancyTenant.Tenant!.Id;
                if (tenancyTenant.TenantId != Guid.Empty)
                {
                    tenancyTenant.Tenant = null;
                }
            }

            if (tenancy.Id == Guid.Empty)
            {
                tenancy.Id = Guid.NewGuid();
                await _tenancyService.AddTenancy(tenancy);
            }
            else
            {
                await _tenancyService.EditTenancy(tenancy);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTenancyTenant(Guid noteId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanDelete);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.DeleteNote(noteId);
            return StatusCode(result.StatusCode, result.Data);
        }


        public async Task<IActionResult> Detail(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.GetTenancyOverView(id);
            return View(result.Data);
        }

        #endregion

        #region TENANCY TENANTS RELATED CODE
        public async Task<IActionResult> Tenants(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.GetTenancyTenants(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddNewExistingTenant(Guid tenancyId, bool isNew)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            return PartialView(new TenancyTenantModel() { TenancyId = tenancyId, IsNew = isNew });
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExistingTenant(TenancyTenantModel tenancyTenant)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            if (tenancyTenant.TenantId != Guid.Empty)
            {
                tenancyTenant.Tenant = null;
            }
            await _tenancyService.LinkTenancyTenant(tenancyTenant);
            return RedirectToAction("Tenants", new { id = tenancyTenant.TenancyId });
        }

        #endregion

        #region TENANCY NOTES RELATED CODE
        public async Task<IActionResult> Notes(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.GetTenancyNotes(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyNote(Guid tenancyId, Guid noteId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            if (noteId != Guid.Empty)
            {
                var result = await _tenancyService.GetTenancyNote(noteId);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new TenancyNoteModel() { TenancyId = tenancyId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyNote([FromForm] TenancyNoteModel notee)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.AddModifyNote(notee);
            return RedirectToAction("Notes", new { id = notee.TenancyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNote(Guid noteId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanDelete);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.DeleteNote(noteId);
            return StatusCode(result.StatusCode, result.Data);
        }
        #endregion

        #region TENANCY DOCUMENT RELATED CODE
        public async Task<IActionResult> Documents(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.GetTenancyDocuments(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyDocument(Guid tenancyId, Guid documentId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            if (documentId != Guid.Empty)
            {
                var result = await _tenancyService.GetTenancyDocument(documentId);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new TenancyDocumentModel() { TenancyId = tenancyId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyDocument([FromForm] TenancyDocumentModel document)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanWrite);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.AddModifyDocument(document);
            return RedirectToAction("Documents", new { id = document.TenancyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDocument(Guid documentId)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.PropertyManagement, PermissionConstant.CanDelete);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.DeleteDocument(documentId);
            return StatusCode(result.StatusCode, result.Data);
        }
        #endregion

        #region TENANCY NOTICE & COMPLAINTS CODE
        public async Task<IActionResult> Notices(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.GetNoticesForTenancyTenants(id);
            return View(result.Data);
        }

        public async Task<IActionResult> Complaints(Guid id)
        {
            var havePermission = await _authService.CheckPermission(PermissionKeyConstant.TenancyManagement, PermissionConstant.CanRead);
            if (!havePermission)
            {
                return RedirectToAction("NotAuthorized", "Auth");
            }
            var result = await _tenancyService.GetComplaintsForTenancyTenants(id);
            return View(result.Data);
        }
        #endregion

        #region TENANCY MESSAGES
        public async Task<IActionResult> Messages(Guid id)
        {
            var result = await _tenancyService.GetTenancyMessages(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyMessage(Guid tenancyId, Guid messageId)
        {
            if (messageId != Guid.Empty)
            {
                var result = await _tenancyService.GetTenancyMessage(messageId);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new TenancyMessageModel() { TenancyId = tenancyId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyMessage([FromForm] TenancyMessageModel messageReq)
        {
            var result = await _tenancyService.AddModifyMessage(messageReq);
            return RedirectToAction("Messages", new { id = messageReq.TenancyId });
        }

        public async Task<IActionResult> ViewMessage(Guid messageId)
        {
            var result = await _tenancyService.GetTenancyMessage(messageId);
            return PartialView(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessage(Guid messageId)
        {
            var result = await _tenancyService.DeleteMessage(messageId);
            return StatusCode(result.StatusCode, result.Data);
        }
        #endregion

        #region TENANCY UNIT UTILITIES
        public async Task<IActionResult> UnitUtilities(Guid id)
        {
            var result = await _tenancyService.GetUnitUtilities(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyUnitUtility(Guid tenancyId, Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _tenancyService.GetUnitUtility(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new UnitUtilityModel() { TenancyId = tenancyId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyUnitUtility(UnitUtilityModel req)
        {
            var result = await _tenancyService.AddModifyUnitUtility(req);
            return RedirectToAction("UnitUtilities", new { id = req.TenancyId });
        }

        public async Task<IActionResult> ViewUnitUtility(Guid id)
        {
            var result = await _tenancyService.GetUnitUtilityView(id);
            return PartialView(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUnitUtility(Guid id)
        {
            var result = await _tenancyService.DeleteUnitUtility(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyUnitUtilityReading(Guid utilityId, Guid tenancyId, Guid id)
        {
            ViewBag.TenancyId = tenancyId;
            if (id != Guid.Empty)
            {
                var result = await _tenancyService.GetUnitUtilityReading(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new UnitUtilityReadingModel() { UnitUtilityId = utilityId });
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddModifyUnitUtilityReading(UnitUtilityReadingModel req, Guid tenancyId)
        {
            var result = await _tenancyService.AddModifyUnitUtilityReading(req);
            return RedirectToAction("UnitUtilities", new { id = tenancyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUnitUtilityReading(Guid id)
        {
            var result = await _tenancyService.DeleteUnitUtilityReading(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyUnitUtilityDocument(Guid utilityId, Guid id)
        {

            if (id != Guid.Empty)
            {
                var result = await _tenancyService.GetUnitUtilityDocument(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new UnitUtilityDocumentModel() { UnitUtilityId = utilityId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyUnitUtilityDocument(UnitUtilityDocumentModel req, Guid tenancyId)
        {
            var result = await _tenancyService.AddModifyUnitUtilityDocument(req);
            return RedirectToAction("UnitUtilities", new { id = tenancyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUnitUtilityDocument(Guid id)
        {
            var result = await _tenancyService.DeleteUnitUtilityDocument(id);
            return StatusCode(result.StatusCode, result.Data);
        }
        #endregion

        #region TENANCY RECURRING CHARGES 
        public async Task<IActionResult> RecurringCharges(Guid id)
        {
            var result = await _tenancyService.GetRecurringCharges(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyRecurringCharge(Guid tenancyId, Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _tenancyService.GetRecurringCharge(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new RecurringChargeModel() { TenancyId = tenancyId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyRecurringCharge(RecurringChargeModel req)
        {
            var result = await _tenancyService.AddModifyRecurringCharge(req);
            return RedirectToAction("RecurringCharges", new { id = req.TenancyId });
        }

        public async Task<IActionResult> ViewRecurringCharge(Guid id)
        {
            var result = await _tenancyService.GetRecurringChargeView(id);
            return PartialView(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecurringCharge(Guid id)
        {
            var result = await _tenancyService.DeleteRecurringCharge(id);
            return StatusCode(result.StatusCode, result.Data);
        }
        #endregion

        #region TENANCY STATEMENT AND TRANSACTIONS
        public async Task<IActionResult> Statements(Guid id)
        {
            var result = await _tenancyService.GetStatements(id);
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyStatement(Guid tenancyId, Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _tenancyService.GetStatement(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new StatementModel() { TenancyId = tenancyId, ReferenceID = RandomAlphaNumbericGenerator.Random(6) });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyStatement(StatementModel req)
        {
            var result = await _tenancyService.AddModifyStatement(req);
            return RedirectToAction("Statements", new { id = req.TenancyId });
        }

        public async Task<IActionResult> ViewStatement(Guid id)
        {
            var result = await _tenancyService.GetStatementView(id);
            return PartialView(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStatement(Guid id)
        {
            var result = await _tenancyService.DeleteStatement(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyStatementTransaction(Guid statementId, Guid tenancyId, Guid id)
        {
            ViewBag.TenancyId = tenancyId;
            if (id != Guid.Empty)
            {
                var result = await _tenancyService.GetStatementTransaction(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new StatementTransactionModel() { StatementId = statementId, InvoiceNumber = RandomAlphaNumbericGenerator.Random(6) });
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddModifyStatementTransaction(StatementTransactionModel req, Guid tenancyId)
        {
            if(req.PaidByTenantId == Guid.Empty)
            {
                req.PaidByTenantId = null;
            }
            var result = await _tenancyService.AddModifyStatementTransaction(req);
            return RedirectToAction("Statements", new { id = tenancyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStatementTransaction(Guid id)
        {
            var result = await _tenancyService.DeleteStatementTransaction(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddModifyStatementDocument(Guid statementId, Guid id)
        {

            if (id != Guid.Empty)
            {
                var result = await _tenancyService.GetStatementDocument(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView(new StatementDocumentModel() { StatementId = statementId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyStatementDocument(StatementDocumentModel req, Guid tenancyId)
        {
            var result = await _tenancyService.AddModifyStatementDocument(req);
            return RedirectToAction("Statements", new { id = tenancyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStatementDocument(Guid id)
        {
            var result = await _tenancyService.DeleteStatementDocument(id);
            return StatusCode(result.StatusCode, result.Data);
        }
        #endregion
    }
}
