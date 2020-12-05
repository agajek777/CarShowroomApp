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
        private readonly IMapper _mapper;

        public ClientRepository(ICarShowroomMongoSettings mongoSettings, IMapper mapper)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _clients = database.GetCollection<Client>(mongoSettings.ClientsCollectionName);
            _mapper = mapper;
        }

        public async Task<ClientDto> AddAsync(ClientDto client)
        {
            await _clients.InsertOneAsync(_mapper.Map<Client>(client));

            var clientInDb = await _clients.FindAsync(c => c.IdentityId == client.IdentityId);

            return _mapper.Map<ClientDto>(await clientInDb.SingleOrDefaultAsync());
        }

        public async Task<bool> ClientExistsAsync(string id)
        {
            var clientInDb = await (await _clients.FindAsync(c => c.IdentityId == id)).SingleOrDefaultAsync();

            return clientInDb == null ? false : true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deletedClient = await _clients.FindOneAndDeleteAsync(c => c.IdentityId == id);

            return deletedClient == null ? false : true;
        }

        public IQueryable<ClientDto> GetAll()
        {
            return _clients.AsQueryable().Select(c => _mapper.Map<ClientDto>(c));
        }

        public async Task<ClientDto> GetAsync(string id)
        {
            var clientInDb = await (await _clients.FindAsync(c => c.IdentityId == id)).FirstOrDefaultAsync();

            return clientInDb == null ? null : _mapper.Map<ClientDto>(clientInDb);
        }

        public async Task<ClientDto> UpdateAsync(string id, ClientDto client)
        {
            var clientToAdd = _mapper.Map<Client>(client);

            var clientInDb = await _clients.FindOneAndReplaceAsync(c => c.IdentityId == id, clientToAdd);

            return clientInDb == null ? null : _mapper.Map<ClientDto>(clientInDb);
        }
    }
}
