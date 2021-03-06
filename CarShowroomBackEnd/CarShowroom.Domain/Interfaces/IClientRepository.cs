﻿using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface IClientRepository<TEntityDto>
    {
        IQueryable<ClientDto> GetAll();
        Task<TEntityDto> GetAsync(string id);
        Task<TEntityDto> AddAsync(TEntityDto entity);
        Task<TEntityDto> UpdateAsync(string id, TEntityDto entity);
        Task<bool> DeleteAsync(string id);
        Task<bool> ClientExistsAsync(string id);
    }
}
