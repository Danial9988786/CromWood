using CromWood.Business.Models;
using CromWood.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CromWood.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IActionResult> Sent()
        {
            var result = await _messageService.GetMessages();
            return View(result.Data);
        }

        public IActionResult Scheduled()
        {
            return View();
        }

        public async Task<IActionResult> ComposeMessage(Guid id)
        {
            var result = await _messageService.GetMessageById(id);
            return PartialView(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> ComposeMessage(MessageModel message)
        {
            var result = await _messageService.ComposeMessage(message);
            return RedirectToAction("Sent");
        }
    }
}
