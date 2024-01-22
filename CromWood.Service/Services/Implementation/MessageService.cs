using AutoMapper;
using CromWood.Business.Helper;
using CromWood.Business.Models;
using CromWood.Business.Models.ViewModel;
using CromWood.Business.Services.Interface;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Interface;

namespace CromWood.Business.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepo, IMapper mapper)
        {
            _messageRepo = messageRepo;
            _mapper = mapper;
        }

        public async Task<AppResponse<IEnumerable<MessageViewModel>>> GetMessages(bool scheduled = false)
        {
            try
            {
                var result = await _messageRepo.GetMessages(scheduled);
                var mappedResult = _mapper.Map<IEnumerable<MessageViewModel>>(result);
                return ResponseCreater<IEnumerable<MessageViewModel>>.CreateSuccessResponse(mappedResult, "Messages loaded successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<IEnumerable<MessageViewModel>>.CreateErrorResponse(null, ex.Message);
            }
        }
        
        public async Task<AppResponse<MessageModel>> GetMessageById(Guid id)
        {
            try
            {
                var result = await _messageRepo.GetMessageById(id);
                var mappedResult = _mapper.Map<MessageModel>(result);
                mappedResult.SelectedRecipients = mappedResult.Recipients.ToList().Select(x => x.RecipientId);
                return ResponseCreater<MessageModel>.CreateSuccessResponse(mappedResult, "Message loaded successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<MessageModel>.CreateErrorResponse(null, ex.Message);
            }
        }

        public async Task<AppResponse<int>> ComposeMessage(MessageModel message)
        {
            try
            {
                message.Recipients.RemoveAll(x => !message.SelectedRecipients.Contains(x.RecipientId));
                foreach (var rec in message.SelectedRecipients)
                {
                    var mess = message.Recipients.Find(x => x.RecipientId == rec);
                    if(mess == null)
                    {
                        message.Recipients.Add(new MessageRecipientModel()
                        {
                            RecipientId = rec
                        });
                    }
                }

                var mappedMessage = _mapper.Map<Message>(message);
                var result = await _messageRepo.ComposeMessage(mappedMessage);
                return ResponseCreater<int>.CreateSuccessResponse(result, "Message composed successfully");
            }
            catch (Exception ex)
            {
                return ResponseCreater<int>.CreateErrorResponse(0, ex.Message);
            }
        }
    }
}
