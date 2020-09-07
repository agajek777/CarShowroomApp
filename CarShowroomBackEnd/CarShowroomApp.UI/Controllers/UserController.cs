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
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userDto)
        {
            if (!ModelState.IsValid)
                BadRequest("UserName and Password is required.");

            var user = new User { UserName = userDto.UserName };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                var userCreated = _mapper.Map<UserForRegisterDto>(await _userManager.FindByNameAsync(user.UserName));
                userCreated.Password = userDto.Password;
                return Ok(userCreated);
            }
            else
                return BadRequest(result.Errors);
        }
    }
}