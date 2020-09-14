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
        IQueryable<TEntityDto> GetAll();
        Task<TEntityDto> Get(int id);
        Task<TEntityDto> Add(TEntityDto entity);
        Task<TEntityDto> Update(int id, TEntityDto entity);
        Task<IActionResult> Delete(int id);
    }
}
