using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpPost("AddRole", Name = "AddRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Role Name");

            if (await _roleManager.RoleExistsAsync(roleDto.Name))
                return Conflict("Provided Role already exists.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleDto.Name));

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleInDb = await _roleManager.FindByNameAsync(roleDto.Name);

            return Ok(_mapper.Map<RoleDto>(roleInDb));
        }
    }
}