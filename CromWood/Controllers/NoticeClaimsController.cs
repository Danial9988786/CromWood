using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Implementation;
using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class NoticeClaimsController : Controller
    {
        private readonly INoticeClaimsService _noticeClaimService;
        public NoticeClaimsController(INoticeClaimsService noticeClaimsService)
        {
            _noticeClaimService = noticeClaimsService;
        }

        public async Task<IActionResult> Notice()
        {
            var result = await _noticeClaimService.GetNotices();
            return View(result.Data ?? new List<NoticeViewModel>());
        }

        public async Task<IActionResult> ViewNotice(Guid id)
        {
            var result = await _noticeClaimService.GetNoticeViewById(id);
            return PartialView(result.Data);
        }

        public async Task<IActionResult> AddModifyNotice(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _noticeClaimService.GetNoticeById(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyNotice(NoticeModel notice)
        {
            var result = await _noticeClaimService.AddModifyNotice(notice);
            return RedirectToAction("Notice");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNotice(Guid id)
        {
            var result = await _noticeClaimService.DeleteNotice(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveNotice(Guid id)
        {
            var result = await _noticeClaimService.ArchiveNotice(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        public async Task<IActionResult> Claim()
        {
            var result = await _noticeClaimService.GetClaims();
            return View(result.Data ?? new List<ClaimViewModel>());
        }

        public async Task<IActionResult> ViewClaim(Guid id)
        {
            var result = await _noticeClaimService.GetClaimViewById(id);
            return PartialView(result.Data);
        }

        public async Task<IActionResult> AddModifyClaim(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _noticeClaimService.GetClaimById(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyClaim(ClaimModel claim)
        {
            var result = await _noticeClaimService.AddModifyClaim(claim);
            return RedirectToAction("Claim");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClaim(Guid id)
        {
            var result = await _noticeClaimService.DeleteClaim(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        public async Task<IActionResult> Archive()
        {
            var result = await _noticeClaimService.GetNotices(true);
            return View(result.Data ?? new List<NoticeViewModel>());
        }
    }
}
