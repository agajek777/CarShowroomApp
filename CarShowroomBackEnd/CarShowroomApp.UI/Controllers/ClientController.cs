using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly ICarService _carService;
        private readonly UserManager<User> _userManager;

        public ClientController(IClientService clientService, ICarService carService, UserManager<User> userManager)
        {
            _clientService = clientService;
            _carService = carService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(ClientDto clientToAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid client model. Please provide Identity Id.");

            if ((await _userManager.FindByIdAsync(clientToAdd.IdentityId)) == null)
                return BadRequest($"No user with prvided user Id {clientToAdd.IdentityId} has been found.");

            foreach (var offer in clientToAdd.Offers)
            {
                if (!await _carService.CarExistsAsync(offer.Id))
                    return BadRequest($"No car with provided Id {offer.Id} has been found.");
            }

            var clientInDb = await _clientService.AddClientAsync(clientToAdd);

            return Ok(clientInDb);
        }
    }
}