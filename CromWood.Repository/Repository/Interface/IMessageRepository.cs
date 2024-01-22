using CromWood.Data.Entities;

namespace CromWood.Data.Repository.Interface
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<Message>> GetMessages(bool scheduled = false);
        public Task<Message> GetMessageById(Guid id);

        public Task<int> ComposeMessage (Message message);
    }
}