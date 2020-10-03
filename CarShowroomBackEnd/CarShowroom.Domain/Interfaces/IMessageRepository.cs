﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface IMessageRepository<TEntityPostDto, TEntityGetDto>
    {
        Task<IQueryable<TEntityGetDto>> GetAllAsync(string senderId, string receiverId);
        Task<bool> AddAsync(TEntityPostDto entity, string senderId);
        Task<bool> DeleteAsync(int id);
    }
}
