using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroomApp.Repository
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
