using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface IMessageRepository<TEntityDto>
    {
        Task<IQueryable<TEntityDto>> GetAllAsync(string senderId, string receiverId);
        Task<bool> AddAsync(TEntityDto entity);
        Task<bool> DeleteAsync(int id);
    }
}
