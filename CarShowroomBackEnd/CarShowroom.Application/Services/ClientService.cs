using CarShowroom.Application.Interfaces;
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
        private readonly IClientRepository<ClientDto> _clientRepository;
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

        public async Task<bool> ClientExistsAsync(string id)
        {
            return await _clientRepository.ClientExistsAsync(id);
        }

        public Task<bool> DeleteClientAsync(string id)
        {
            throw new NotImplementedException();
        }

        public PagedList<ClientDto> GetAllClients(QueryParameters queryParameters)
        {
            return PagedList<ClientDto>.ToPagedList(_clientRepository.GetAll(),
                                                queryParameters.PageNumber,
                                                queryParameters.PageSize);
        }

        public async Task<ClientDto> GetClientAsync(string id)
        {
            return await _clientRepository.GetAsync(id);
        }

        public Task<ClientDto> UpdateClientAsync(string id, ClientDto clientToUpdate)
        {
            return _clientRepository.UpdateAsync(id, clientToUpdate);
        }
    }
}
