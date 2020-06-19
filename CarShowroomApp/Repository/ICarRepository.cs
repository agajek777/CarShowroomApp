using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroomApp.Repository
{
    public interface ICarRepository<TEntity, TEntityDto>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntityDto entity);
        void Delete(TEntity entity);
    }
}
