using AutoMapper;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Messaging;
using CarShowroom.Infra.Data.Context;
using log4net.Repository.Hierarchy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories
{
    class MessageRepository : IMessageRepository<MessagePostDto>
    {
        private readonly DatabaseContext<User, Role> _db;
        private readonly IMapper _mapper;
        private readonly Logger<MessageRepository> _logger;

        public MessageRepository(DatabaseContext<User, Role> db, IMapper mapper, Logger<MessageRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> AddAsync(MessagePostDto entity)
        {
            if (!await CheckConnectionAsync())
                throw new DataException("Can't connect to the db.");

            var message = _mapper.Map<Message>(entity);

            message.Sent = DateTime.Now;

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
            }

            return true;
        }

        public Task<IQueryable<MessagePostDto>> GetAllAsync(string senderId, string receiverId)
        {
            throw new NotImplementedException();
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
