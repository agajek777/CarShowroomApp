using AutoMapper;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories
{
    public class ClientRepository : IClientRepository<ClientDto>
    {
        private readonly IMongoCollection<Client> _clients;
        private readonly ICarShowroomMongoSettings _mongoSettings;
        private readonly IMapper _mapper;

        public ClientRepository(ICarShowroomMongoSettings mongoSettings, IMapper mapper)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _clients = database.GetCollection<Client>(mongoSettings.ClientsCollectionName);
            _mongoSettings = mongoSettings;
            _mapper = mapper;
        }

        public async Task<ClientDto> AddAsync(ClientDto client)
        {
            await _clients.InsertOneAsync(_mapper.Map<Client>(client));

            var clientInDb = await _clients.FindAsync(c => c.IdentityId == client.IdentityId);

            return _mapper.Map<ClientDto>(await clientInDb.SingleOrDefaultAsync());
        }

        public Task<bool> ClientExistsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<ClientDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> UpdateAsync(string id, ClientDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
