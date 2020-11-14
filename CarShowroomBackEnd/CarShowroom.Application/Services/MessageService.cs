using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.Application.Services
{
    public class MessageService : IMessageService<MessagePostDto, MessageGetDto>
    {
        private readonly IMessageRepository<MessagePostDto, MessageGetDto> _messageRepository;
        private readonly UserManager<User> _userManager;

        public MessageService(IMessageRepository<MessagePostDto, MessageGetDto> messageRepository, UserManager<User> userManager)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
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

        public async Task<ICollection<MessageGetDto>> GetAllAsync(string senderId, string receiverId)
        {
            try
            {
                var sender = await _userManager.FindByIdAsync(senderId);
                var receiver = await _userManager.FindByIdAsync(receiverId);

                var msgs =  await _messageRepository.GetAllAsync(senderId, receiverId);
                var messages = await msgs.ToListAsync();

                foreach (var msg in messages)
                {
                    msg.SenderName = msg.SenderId == senderId ? sender.UserName : receiver.UserName;
                }

                return messages;
            }
            catch (DataException)
            {
                throw;
            }
        }
    }
}
