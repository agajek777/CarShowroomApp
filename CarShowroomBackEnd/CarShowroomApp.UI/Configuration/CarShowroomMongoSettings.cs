using CarShowroom.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.UI.Configuration
{
    public class CarShowroomMongoSettings : ICarShowroomMongoSettings
    {
        public string ClientsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
