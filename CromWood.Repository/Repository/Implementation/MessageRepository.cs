using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CromWood.Data.Repository.Implementation
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(CromwoodContext context) : base(context) { }

        public async Task<IEnumerable<Message>> GetMessages(bool scheduled = false)
        {
            return await _context.Messages.Where(x => x.IsScheduled == scheduled).Include(x => x.Recipients).ThenInclude(x=>x.Recipient).ToListAsync();
        }

        public async Task<Message> GetMessageById(Guid id)
        {
            return await _context.Messages.Include(x => x.Recipients).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> ComposeMessage(Message message)
        {

            try
            {
                if (message.Id == Guid.Empty)
                {
                    await _context.Messages.AddAsync(message);
                }
                else
                {
                    _context.Messages.Update(message);
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
