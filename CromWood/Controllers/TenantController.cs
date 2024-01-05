using CromWood.Business.Models;
using CromWood.Business.Services.Implementation;
using CromWood.Business.Services.Interface;
using CromWood.Helper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    [Authorize]
    public class TenantController : Controller
    {
        private readonly ITenantService _tenantService;
        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _tenantService.GetTenantForList();
            return View(result.Data);
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
    }
}
