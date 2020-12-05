using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(ClientDto clientToAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var clientInDb = await _clientService.AddClientAsync(clientToAdd);

            return Ok(clientInDb);
        }

    }
}
