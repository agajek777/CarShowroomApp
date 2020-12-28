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

        protected virtual bool CheckConnection()
        {
            _logger.LogInformation("CheckConnectionAsync() checking connection to the database.");

            if(_db.Database == null)
            {
                _logger.LogWarning("CheckConnectionAsync(): Could not connect to the database.");
                return false;
            }

            return true;
        }
    }
}
