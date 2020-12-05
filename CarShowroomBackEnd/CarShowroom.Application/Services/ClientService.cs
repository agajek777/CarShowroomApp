﻿using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Parameters;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly Domain.Interfaces.IClientRepository<ClientDto> _clientRepository;
        private readonly UserManager<User> _userManager;

        public ClientService(IClientRepository<ClientDto> clientRepository, UserManager<User> userManager)
        {
            _clientRepository = clientRepository;
            _userManager = userManager;
        }
        public async Task<ClientDto> AddClientAsync(ClientDto clientToAdd)
        {
            var user = await _userManager.FindByIdAsync(clientToAdd.IdentityId);

            clientToAdd.UserName = user.UserName;

            return await _clientRepository.AddAsync(clientToAdd);
        }

        public Task<bool> ClientExistsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClientAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ClientDto>> GetAllClientsAsync(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> GetClientAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ClientDto> UpdateClientAsync(string id, ClientDto clientToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
