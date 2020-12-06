using CarShowroom.Domain.Models.DTO;
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
        public Task<ClientDto> GetClientAsync(string id);
        public Task<ClientDto> AddClientAsync(ClientDto clientToAdd);
        public Task<ClientDto> UpdateClientAsync(string id, ClientDto clientToUpdate);
        public Task<bool> DeleteClientAsync(string id);
        public Task<bool> ClientExistsAsync(string id);
        public Task<bool> AddCarOffer(string userId, int? carId);
    }
}
