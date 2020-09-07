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
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpGet("GetUsersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = _userManager.Users;
            var roles = _roleManager.Roles;

            var usersWithRoles = new List<UserWithRoles>();
            
            foreach (var user in users)
            {
                usersWithRoles.Add(new UserWithRoles()
                {
                    User = _mapper.Map<UserDto>(user),
                    Roles = (await _userManager.GetRolesAsync(user)).Select(r => new RoleDto()
                    { 
                        Name = r
                    })
                });
            }

            return Ok(usersWithRoles);
        }
    }
}