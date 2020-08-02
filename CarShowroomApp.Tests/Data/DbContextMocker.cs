using CarShowroomApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroomApp.Tests.Data
{
    public static class DbContextMocker
    {
        public static DatabaseContext GetDatabaseContext(string dbname)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName: dbname).Options;

            // Create instance of DbContext
            var dbContext = new DatabaseContext(options);

            // Seed data
            dbContext.Seed();

            return dbContext;
        }
    }
}
