using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Domain.Interfaces
{
    public interface ICarShowroomMongoSettings
    {
        string ClientsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
