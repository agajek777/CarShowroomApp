using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using System.Threading.Tasks;

namespace CarShowroom.Application.Interfaces
{
    public interface ICarService
    {
        public Task<PagedList<CarDto>> GetAllCarsAsync(QueryParameters queryParameters);
        public Task<CarDto> GetCarAsync(int id);
        public Task<CarDto> AddCarAsync(string id, CarDto carToAdd);
        public Task<CarDto> UpdateCarAsync(int id, CarDto carToUpdate);
        public Task<bool> DeleteCarAsync(string userId, int id);
        public Task<bool> CarExistsAsync(int id);
    }
}
