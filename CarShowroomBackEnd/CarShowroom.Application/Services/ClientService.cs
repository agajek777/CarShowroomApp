﻿using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Parameters;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            ClientDto clientInDb;

            try
            {
                clientInDb = await _clientRepository.GetAsync(userId);
            }
            catch (DataException)
            {
                throw;
            }

            clientInDb.Offers.Add(new Offer { Id = (int)carId });

            var outcome = await _clientRepository.UpdateAsync(userId, clientInDb);

            return outcome == null ? false : true;
        }

        public async Task<bool> DeleteCarOffer(string userId, int? carId)
        {
            ClientDto clientInDb;

            try
            {
                clientInDb = await _clientRepository.GetAsync(userId);
            }
            catch (DataException)
            {
                throw;
            }

            clientInDb.Offers.Remove(
                clientInDb.Offers.First(
                    o => o.Id == (int)carId
                    )
                );

            var outcome = await _clientRepository.UpdateAsync(userId, clientInDb);

            return outcome == null ? false : true;
        }

        public async Task<bool> CheckIfOwnerAsync(string userId, int carId)
        {
            ClientDto clientInDb;

            try
            {
                clientInDb = await _clientRepository.GetAsync(userId);
            }
            catch (DataException)
            {
                throw;
            }

            foreach (var offer in clientInDb.Offers)
            {
                if (offer.Id == carId)
                    return true;
            }

            return false;
        }

        public async Task<ClientDto> AddClientAsync(ClientDto clientToAdd)
        {
            try
            {
                return await _clientRepository.AddAsync(clientToAdd);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<bool> ClientExistsAsync(string id)
        {
            try
            {
                return await _clientRepository.ClientExistsAsync(id);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public Task<bool> DeleteClientAsync(string id)
        {
            try
            {
                return _clientRepository.DeleteAsync(id);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<PagedList<ClientDto>> GetAllClientsAsync(QueryParameters queryParameters)
        {
            try
            {
                return PagedList<ClientDto>.ToPagedList(await _clientRepository.GetAllAsync(),
                                                    queryParameters.PageNumber,
                                                    queryParameters.PageSize);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public async Task<ClientDto> GetClientAsync(string id)
        {
            try
            {
                return await _clientRepository.GetAsync(id);
            }
            catch (DataException)
            {
                throw;
            }
        }

        public Task<ClientDto> UpdateClientAsync(string id, ClientDto clientToUpdate)
        {
            try
            {
                return _clientRepository.UpdateAsync(id, clientToUpdate);
            }
            catch (DataException)
            {
                throw;
            }
        }
    }
}
