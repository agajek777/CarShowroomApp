﻿using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.Application.Interfaces
{
    public interface IMessageService<TEntityPostDto, TEntityGetDto>
    {
        Task<IQueryable<TEntityGetDto>> GetAllAsync(string senderId, string receiverId);
        Task<bool> AddAsync(TEntityPostDto entity, string senderId);
        Task<bool> DeleteAsync(int id);
    }
}