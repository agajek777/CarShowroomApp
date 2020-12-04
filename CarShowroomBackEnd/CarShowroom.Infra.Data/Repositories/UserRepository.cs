using CarShowroom.Domain.Models.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Infra.Data.Repositories
{
    public class UserRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public Client Add(Client client)
        {
            
        }
    }
}
