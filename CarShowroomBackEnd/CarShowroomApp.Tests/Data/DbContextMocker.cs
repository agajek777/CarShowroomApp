using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CarShowroomApp.Tests.Data
{
    public static class DbContextMocker
    {
        public static DatabaseContext<User, Role> GetDatabaseContext(string dbname)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<DatabaseContext<User, Role>>().UseInMemoryDatabase(databaseName: dbname).Options;

            // Create instance of DbContext
            var dbContext = new DatabaseContext<User, Role>(options);

            // Seed data
            dbContext.Seed();

            return dbContext;
        }
    }
}
