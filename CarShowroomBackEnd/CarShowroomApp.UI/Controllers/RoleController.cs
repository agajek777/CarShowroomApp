using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            return Ok(_roleManager.Roles.ToList().Select(r => _mapper.Map<RoleDto>(r)));
        }

        [HttpPost("AddRole", Name = "AddRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Role Name");

            if (await _roleManager.RoleExistsAsync(roleDto.Name))
                return Conflict("Provided Role already exists.");

            var result = await _roleManager.CreateAsync(new Role() { Name = roleDto.Name });

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleInDb = await _roleManager.FindByNameAsync(roleDto.Name);

            return Ok(_mapper.Map<RoleDto>(roleInDb));
        }

        [HttpDelete("DeleteRole", Name = "DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] RoleDto roleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Role Name");

            if (!await _roleManager.RoleExistsAsync(roleDto.Name))
                return NotFound("Provided Role does not exist.");

            var roleInDb = await _roleManager.FindByNameAsync(roleDto.Name);

            var result = await _roleManager.DeleteAsync(roleInDb);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [HttpPut("EditRole/{roleName}", Name = "EditRole")]
        public async Task<IActionResult> EditRole([FromBody] RoleDto roleDto, string roleName)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Role Name");

            if (!await _roleManager.RoleExistsAsync(roleName))
                return NotFound($"No role with '{roleName}' name found.");

            if (roleName == roleDto.Name)
                return BadRequest();

            var roleInDb = await _roleManager.FindByNameAsync(roleName);

            _mapper.Map<RoleDto, Role>(roleDto, roleInDb);

            var result = await _roleManager.UpdateAsync(roleInDb);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(_mapper.Map<RoleDto>(roleInDb));
        }
    }
}