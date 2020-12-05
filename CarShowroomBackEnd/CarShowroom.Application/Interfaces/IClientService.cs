using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Application.Interfaces
{
    public interface IClientService
    {
        public Task<PagedList<ClientDto>> GetAllClientsAsync(QueryParameters queryParameters);
        public Task<ClientDto> GetClientAsync(string id);
        public Task<ClientDto> AddClientAsync(ClientDto carToAdd);
        public Task<ClientDto> UpdateClientAsync(string id, ClientDto carToUpdate);
        public Task<bool> DeleteClientAsync(string id);
        public Task<bool> ClientExistsAsync(string id);
    }
}
