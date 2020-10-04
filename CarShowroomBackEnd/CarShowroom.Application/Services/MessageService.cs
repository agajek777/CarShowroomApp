using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.Application.Services
{
    public class MessageService : IMessageService<MessagePostDto, MessageGetDto>
    {
        private readonly IMessageRepository<MessagePostDto, MessageGetDto> _messageRepository;

        public MessageService(IMessageRepository<MessagePostDto, MessageGetDto> messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<bool> AddAsync(MessagePostDto entity, string senderId)
        {
            try
            {
                return await _messageRepository.AddAsync(entity, senderId);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _messageRepository.DeleteAsync(id);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<IQueryable<MessageGetDto>> GetAllAsync(string senderId, string receiverId)
        {
            try
            {
                return await _messageRepository.GetAllAsync(senderId, receiverId);
            }
            catch (DataException)
            {
                throw;
            }
        }
    }
}
