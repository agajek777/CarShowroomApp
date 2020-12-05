using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Infra.Data.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<Client> _clients;

        public UserRepository(ICarShowroomMongoSettings mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _clients = database.GetCollection<Client>(mongoSettings.ClientsCollectionName);
        }

        public async Task<Client> AddAsync(Client client)
        {
            await _clients.InsertOneAsync(client);

            var clientInDb = await _clients.FindAsync(c => c.IdentityId == client.IdentityId);

            return await clientInDb.SingleOrDefaultAsync();
        }
    }
}
