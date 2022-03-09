using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Parameters;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Threading.Tasks;

namespace CarShowroom.Application.Services
{
    public delegate Task<bool> CarCDHandler(string userId, int? carId);
    public class CarService : ICarService
    {
        private readonly ICarRepository<CarDto> _carRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;

        public event CarCDHandler OnCarAdded;
        public event CarCDHandler OnCarDeleted;

        public CarService(ICarRepository<CarDto> carRepository, IClientService clientService, UserManager<User> userManager, ILogger<CarService> logger)
        {
            _carRepository = carRepository;
            _userManager = userManager;
            _logger = logger;

            OnCarAdded += clientService.AddCarOfferAsync;
            OnCarDeleted += clientService.DeleteCarOfferAsync;
        }
        public async Task<CarDto> AddCarAsync(string id, CarDto carToAdd)
        {
            CarDto addedCar;

            carToAdd.OwnerId = id;

            try
            {
                _logger.LogInformation(
                    "Executing _carRepository.AddAsync(carToAdd) with params {Parameters}",
                    carToAdd.ToString()
                );

                addedCar = await _carRepository.AddAsync(carToAdd);
            }
            catch (DataException)
            {
                throw;
            }

            if (addedCar != null)
            {
                _logger.LogInformation(
                    "Adding offer {OfferId} to Client's account. Client Id = {ClientId}",
                    addedCar.Id, id
                );

                var result = await OnCarAdded?.Invoke(id, addedCar.Id);

                if (!result)
                {
                    _logger.LogWarning("Error while adding of an offer to client's account. Rollback.");
                    await _carRepository.DeleteAsync((int)addedCar.Id);
                }
            }

            return addedCar;
        }

        public async Task<bool> DeleteCarAsync(string userId, int id)
        {
            bool result;
            try
            {
                 result = await _carRepository.DeleteAsync(id);
            }
            catch (DataException)
            {
                throw;
            }

            if (!result)
                return result;

            result = await OnCarDeleted?.Invoke(userId, id);

            return result;
        }

        public async Task<PagedList<CarDto>> GetAllCarsAsync(QueryParameters queryParameters)
        {
            try
            {
                return PagedList<CarDto>.ToPagedList(await _carRepository.GetAllAsync(),
                                                queryParameters.PageNumber,
                                                queryParameters.PageSize);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<CarWithUserDetails> GetCarAsync(int id)
        {
            CarWithUserDetails result;
            CarDto carDto;
            User userDto;

            try
            {
                carDto = await _carRepository.GetAsync(id);
            }
            catch (DataException)
            {
                throw;
            }

            userDto = await _userManager.FindByIdAsync(carDto.OwnerId);

            if (carDto == null || userDto == null)
                { return null; }

            result = new CarWithUserDetails()
            {
                Car = carDto,
                UserId = userDto.Id,
                UserName = userDto.UserName
            };

            return result;
        }

        public async Task<CarDto> UpdateCarAsync(int id, CarDto carToUpdate)
        {
            try
            {
                return await _carRepository.UpdateAsync(id, carToUpdate);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<bool> CarExistsAsync(int id)
        {
            try
            {
                return await _carRepository.CarExistsAsync(id);
            }
            catch (DataException)
            {
                throw;
            }
        }
    }
}
