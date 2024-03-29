﻿using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Application.Interfaces
{
    public interface IClientService
    {
        public PagedList<ClientDto> GetAllClients(QueryParameters queryParameters);
        public ClientWithUsername GetClient(string id);
        public Task<ClientDto> AddClientAsync(ClientDto clientToAdd);
        public Task<ClientDto> UpdateClientAsync(string id, ClientDto clientToUpdate);
        public Task<bool> DeleteClientAsync(string id);
        public Task<bool> ClientExistsAsync(string id);
        public Task<bool> AddCarOfferAsync(string userId, int? carId);
        public Task<bool> DeleteCarOfferAsync(string userId, int? carId);
        public Task<bool> CheckIfOwnerAsync(string userId, int carId);
    }
}
