using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
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

        public async Task<bool> AddCarOffer(string userId, int? carId)
        {
            var clientInDb = await _clientRepository.GetAsync(userId);

            clientInDb.Offers.Add(new Offer { Id = (int)carId });

            var outcome = await _clientRepository.UpdateAsync(userId, clientInDb);

            return outcome == null ? false : true;
        }

        public async Task<ClientDto> AddClientAsync(ClientDto clientToAdd)
        {
            return await _clientRepository.AddAsync(clientToAdd);
        }

        public async Task<bool> ClientExistsAsync(string id)
        {
            return await _clientRepository.ClientExistsAsync(id);
        }

        public Task<bool> DeleteClientAsync(string id)
        {
            return _clientRepository.DeleteAsync(id);
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
