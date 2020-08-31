using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface ICarRepository<TEntity, TEntityDto>
    {
        IEnumerable<TEntityDto> GetAll();
        Task<TEntityDto> Get(int id);
        Task<TEntityDto> Add(TEntityDto entity);
        Task<TEntityDto> Update(int id, TEntityDto entity);
        Task<IActionResult> Delete(int id);
    }
}
