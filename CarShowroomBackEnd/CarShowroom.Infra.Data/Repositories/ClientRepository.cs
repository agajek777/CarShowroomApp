using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Repositories.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories
{
    public class ClientRepository : Repository, IClientRepository<ClientDto>
    {
        private readonly IMongoCollection<Client> _clients;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDb;

        public ClientRepository(ICarShowroomMongoSettings mongoSettings, IMapper mapper, ILogger<ClientRepository> logger) : base(mapper, logger)
        {
            _mongoClient = new MongoClient(mongoSettings.ConnectionString);
            _mongoDb = _mongoClient.GetDatabase(mongoSettings.DatabaseName);

            _clients = _mongoDb.GetCollection<Client>(mongoSettings.ClientsCollectionName);
        }

        public ClientRepository(IMongoClient mongoClient, IMongoDatabase mongoDatabase, IMongoCollection<Client> mongoCollection, IMapper mapper, ILogger<ClientRepository> logger) : base(mapper, logger)
        {
            _mongoClient = mongoClient;
            _mongoDb = mongoDatabase;
            _clients = mongoCollection;
        }

        public async Task<ClientDto> AddAsync(ClientDto client)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            await _clients.InsertOneAsync(_mapper.Map<Client>(client));

            var clientInDb = await _clients.FindAsync(c => c.IdentityId == client.IdentityId);

            return _mapper.Map<ClientDto>(await clientInDb.SingleOrDefaultAsync());
        }

        public async Task<bool> ClientExistsAsync(string id)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var clientInDb = await (await _clients.FindAsync(c => c.IdentityId == id)).SingleOrDefaultAsync();

            return clientInDb == null ? false : true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var clientToDelete = await _clients.DeleteOneAsync(c => c.IdentityId == id);

            var outcome = await (await _clients.FindAsync(c => c.IdentityId == id)).SingleOrDefaultAsync();

            return outcome == null ? true : false;
        }

        public IQueryable<ClientDto> GetAll()
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            return _clients.AsQueryable().ProjectTo<ClientDto>(_mapper.ConfigurationProvider, c => _mapper.Map<ClientDto>(c));
        }

        public async Task<ClientDto> GetAsync(string id)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var clientInDb = await (await _clients.FindAsync(c => c.IdentityId == id)).FirstOrDefaultAsync();

            return clientInDb == null ? null : _mapper.Map<ClientDto>(clientInDb);
        }

        public async Task<ClientDto> UpdateAsync(string id, ClientDto client)
        {
            if (!CheckConnection())
                throw new DataException("Can't connect to the db.");

            var clientToAdd = _mapper.Map<Client>(client);

            var result = await _clients.ReplaceOneAsync(c => c.IdentityId == id, clientToAdd);

            var clientInDb = await (await _clients.FindAsync(c => c.IdentityId == id)).SingleOrDefaultAsync();

            return !result.IsAcknowledged ? null : _mapper.Map<ClientDto>(clientInDb);
        }

        protected override bool CheckConnection()
        {
            _logger.LogInformation("CheckConnectionAsync() checking connection to the database.");

            if (_mongoClient == null)
                return false;

            try
            {
                _mongoClient.ListDatabases();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "CheckConnectionAsync() got exception: {Message}", ex.Message);
                return false;
            }

            return true;
        }
    }
}
