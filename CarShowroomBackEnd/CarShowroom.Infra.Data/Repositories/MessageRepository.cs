using AutoMapper;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Messaging;
using CarShowroom.Infra.Data.Context;
using CarShowroom.Infra.Data.Repositories.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories
{
    public class MessageRepository : Repository, IMessageRepository<MessagePostDto, MessageGetDto>
    {
        public MessageRepository(DatabaseContext<User, Role> db, IMapper mapper, ILogger<MessageRepository> logger) : base(db, mapper, logger)
        {

        }

        public async Task<bool> AddAsync(MessagePostDto entity, string senderId)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var message = _mapper.Map<Message>(entity);

            message.Sent = DateTime.Now;
            message.SenderId = senderId;

            await _db.Messages.AddAsync(message);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "AddAsync() got exception: {Message}", ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var message = await _db.Messages.FindAsync(id);

            if (message == null)
                return false;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DeleteAsync() got exception: {Message}", ex.Message);
                return false;
            }

            return true;
        }

        public async Task<IQueryable<MessageGetDto>> GetAllAsync(string senderId, string receiverId)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            return from message in _db.Messages
                    where (message.SenderId == senderId && message.ReceiverId == receiverId)
                        ||
                        (message.SenderId == receiverId && message.ReceiverId == senderId)
                    select _mapper.Map<MessageGetDto>(message);
        }
    }
}
