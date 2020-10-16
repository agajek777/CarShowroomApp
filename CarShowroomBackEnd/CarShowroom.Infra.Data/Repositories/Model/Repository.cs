using AutoMapper;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories.Model
{
    public abstract class Repository
    {
        protected readonly DatabaseContext<User, Role> _db;
        protected readonly IMapper _mapper;
        protected readonly ILogger<MessageRepository> _logger;
        public Repository(DatabaseContext<User, Role> db, IMapper mapper, ILogger<MessageRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
        protected async Task<bool> CheckConnectionAsync()
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
    }
}
