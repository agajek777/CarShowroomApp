using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface IClientRepository<TEntityDto>
    {
        Task<IQueryable<TEntityDto>> GetAllAsync();
        Task<TEntityDto> GetAsync(string id);
        Task<TEntityDto> AddAsync(TEntityDto entity);
        Task<TEntityDto> UpdateAsync(string id, TEntityDto entity);
        Task<bool> DeleteAsync(string id);
        Task<bool> ClientExistsAsync(string id);
    }
}
