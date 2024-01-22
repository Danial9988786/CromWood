using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;

namespace CromWood.Business.Services.Interface
{
    public interface IMessageService
    {
        public Task<AppResponse<IEnumerable<MessageViewModel>>> GetMessages(bool scheduled = false);
        public Task<AppResponse<MessageModel>> GetMessageById(Guid id);
        public Task<AppResponse<int>> ComposeMessage(MessageModel message);
    }
}
