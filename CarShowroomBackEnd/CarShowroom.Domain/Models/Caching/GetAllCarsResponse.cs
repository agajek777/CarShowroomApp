using CarShowroom.Domain.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace CarShowroom.Domain.Models.Caching
{
    public class GetAllCarsResponse
    {
        public List<CarDto> Value { get; set; }
        public List<KeyValuePair<string, StringValues>> Headers { get; set; }
    }
}
