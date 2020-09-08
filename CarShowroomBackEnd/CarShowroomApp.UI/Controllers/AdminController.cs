using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRolesEnum.Admin)]
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

            var usersWithRoles = new List<UserWithRolesDto>();
            
            foreach (var user in users)
            {
                usersWithRoles.Add(new UserWithRolesDto()
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
        [HttpPost("GetUserWithRoles")]
        public async Task<IActionResult> GetUserWithRoles(UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);

            if (user == null)
                return BadRequest($"No user with UserName {userDto.UserName} found.");

            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(new UserWithRolesDto
            {
                User = _mapper.Map<UserDto>(user),
                Roles = userRoles.Select(r => new RoleDto { Name = r })
            });
        }

        [HttpPost("EditUserRoles")]
        public async Task<IActionResult> EditUserRoles(UserWithRolesDto userWithRoles)
        {
            var user = await _userManager.FindByNameAsync(userWithRoles.User.UserName);

            if (user == null)
                return BadRequest($"No user with UserName {userWithRoles.User.UserName} found.");
            
            var backupUserRoles = await _userManager.GetRolesAsync(user);

            var roles = userWithRoles.Roles.ToList().Select(r => r.Name);

            var resultRemove = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

            if (roles.Count() == 0)
            {
                if (!resultRemove.Succeeded)
                    return BadRequest(resultRemove.Errors);

                return Ok($"User {user.UserName} from now on belongs to none of roles.");
            }

            try
            {
                var resultAdd = await _userManager.AddToRolesAsync(user, roles);
            }
            catch (InvalidOperationException ex)
            {
                // Backup previous roles of the user
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

                var result = await _userManager.AddToRolesAsync(user, backupUserRoles);
                //
                return BadRequest(new { Error = ex.Message, User = await GetUserWithRoles(_mapper.Map<UserDto>(user)) });
            }

            return await GetUserWithRoles(_mapper.Map<UserDto>(user));
        }
    }
}