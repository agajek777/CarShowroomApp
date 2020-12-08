using AutoMapper;
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
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("GetUsers/{query}")]
        public async Task<IActionResult> SearchUsersByName(string query)
        {
            var users = await _userManager.GetUsersInRoleAsync(UserRolesEnum.User);

            var outcome = from user in users
                          where user.UserName.ToLower().Contains(query.ToLower().Trim())
                          select _mapper.Map<UserWithIdDto>(user);

            return Ok(outcome);
        }
    }
}
