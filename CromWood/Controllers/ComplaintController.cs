using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IComplaintService _complaintService;
        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _complaintService.GetComplaints();
            return View(result.Data);
        }

        public async Task<IActionResult> AddModifyComplaint(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _complaintService.GetComplaintById(id);
                return PartialView(result.Data);
            }
            else
            {
                return PartialView();
            }
        }
        public async Task<IActionResult> ViewComplaint(Guid id)
        {
            var result = await _complaintService.GetComplaintViewById(id);
            return PartialView(result.Data);
        }


        [HttpPost]
        public async Task<IActionResult> AddModifyComplaint([FromForm] ComplaintModel complaint)
        {
            var result = await _complaintService.AddModifyComplaint(complaint);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComplaint(Guid id)
        {
            var result = await _complaintService.DeleteComplaint(id);
            return StatusCode(result.StatusCode, result.Data);
        }


        // Code for Comments inside Complaints

        [HttpGet]
        public async Task<IActionResult> Comments(Guid complaintId)
        {
            var result = await _complaintService.GetComplaintComments(complaintId);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var result = await _complaintService.GetComplaintById(id);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddModifyComplaintComment(ComplaintCommentModel comment)
        {
            var result = await _complaintService.AddModifyComplaintComment(comment);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComplaintComment(Guid id)
        {
            var result = await _complaintService.DeleteComplaintComment(id);
            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
