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
        protected readonly ILogger<Repository> _logger;
        public Repository(DatabaseContext<User, Role> db, IMapper mapper, ILogger<Repository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public Repository(IMapper mapper, ILogger<Repository> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        protected virtual async Task<bool> CheckConnectionAsync()
        {
            _logger.LogInformation("CheckConnectionAsync() checking connection to the database.");

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
