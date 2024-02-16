using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Interface;
using CromWood.Helper;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class FinancialController : Controller
    {
        private readonly ITenancyService _tenancyService;
        public FinancialController(ITenancyService tenancyService)
        {
            _tenancyService = tenancyService;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> RentManagement()
        {
            var result = await _tenancyService.GetStatements();
            return View(result.Data);
        }
        public async Task<IActionResult> PaymentPlan()
        {
            var result = await _tenancyService.GetPaymentPlans();
            return View(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> AddModifyPaymentPlan(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _tenancyService.GetPaymentPlan(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyPaymentPlan(PaymentPlanModel req)
        {
            req.NoOfInstallment = Convert.ToInt32(Math.Ceiling(req.Amount / req.InstallmentAmount));
            var result = await _tenancyService.AddModifyPaymentPlan(req);
            return RedirectToAction("PaymentPlan");
        }
        public async Task<IActionResult> ViewPaymentPlan(Guid id)
        {
            var result = await _tenancyService.GetPaymentPlanView(id);
            return PartialView(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePaymentPlan(Guid id)
        {
            var result = await _tenancyService.DeletePaymentPlan(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        public async Task<IActionResult> HousingBenefit()
        {
            var result = await _tenancyService.GetHousingBenefitTenancy();
            var payouts = await _tenancyService.GetHousingBenefitStatments();
            ViewBag.Payouts = payouts.Data==null?new List<StatementViewModel>(): payouts.Data.ToList();
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> AddPayout()
        {
            var tenancies = await _tenancyService.GetHousingBenefitTenancy();
            var tenancyPayout = new List<PayoutTenantModel>();
            foreach (var tenancy in tenancies.Data)
            {
                tenancyPayout.Add(
                    new PayoutTenantModel()
                    {
                        TenancyId = tenancy.Id,
                        TenancyName = tenancy.TenancyId,
                        TenantName = tenancy.TenancyTenants[0].Tenant.FullName,
                        RentAmount = tenancy.RentAmount,
                        RentFrequency = tenancy.RentFrequency.Name
                    });
            }
            var payout = new PayoutModel()
            {
                Payouts = tenancyPayout,
                PayoutID = $"PP{RandomAlphaNumbericGenerator.Random(4)}"
            };
            return PartialView(payout);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayout(PayoutModel payout)
        {
            var result = await _tenancyService.CreateTenancyStatements(payout);
            return RedirectToAction("HousingBenefit");
        }

        public IActionResult RevenueExpense()
        {
            return View();
        }
    }
}
