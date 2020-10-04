using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface ICarRepository<TEntityDto>
    {
        Task<IQueryable<TEntityDto>> GetAllAsync();
        Task<TEntityDto> GetAsync(int id);
        Task<TEntityDto> AddAsync(TEntityDto entity);
        Task<TEntityDto> UpdateAsync(int id, TEntityDto entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> CarExistsAsync(int id);
    }
}
