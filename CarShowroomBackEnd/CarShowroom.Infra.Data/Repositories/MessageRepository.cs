using AutoMapper;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Messaging;
using CarShowroom.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories
{
    public class MessageRepository : IMessageRepository<MessagePostDto, MessageGetDto>
    {
        private readonly DatabaseContext<User, Role> _db;
        private readonly IMapper _mapper;
        private readonly ILogger<MessageRepository> _logger;

        public MessageRepository(DatabaseContext<User, Role> db, IMapper mapper, ILogger<MessageRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> AddAsync(MessagePostDto entity, string senderId)
        {
            if (!await CheckConnectionAsync())
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
            if (!await CheckConnectionAsync())
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
            if (!await CheckConnectionAsync())
                throw new DataException("Can't connect to the db.");

            return from message in _db.Messages
                    where (message.SenderId == senderId && message.ReceiverId == receiverId)
                        ||
                        (message.SenderId == receiverId && message.ReceiverId == senderId)
                    select _mapper.Map<MessageGetDto>(message);
        }

        #region HelperMethods
        private async Task<bool> CheckConnectionAsync()
        {
            try
            {
                await _db.GetService<IRelationalDatabaseCreator>().CanConnectAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogWarning(ex, "CheckConnectionAsync() got exception: {Message}", ex.Message);
                return false;
            }
            return true;
        }
        #endregion
    }
}
