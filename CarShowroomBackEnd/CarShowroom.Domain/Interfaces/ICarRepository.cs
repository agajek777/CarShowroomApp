using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface ICarRepository<TEntity, TEntityDto>
    {
        Task<IQueryable<TEntityDto>> GetAllAsync();
        Task<TEntityDto> GetAsync(int id);
        Task<TEntityDto> AddAsync(TEntityDto entity);
        Task<TEntityDto> UpdateAsync(int id, TEntityDto entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> CarExistsAsync(int id)
    }
}
