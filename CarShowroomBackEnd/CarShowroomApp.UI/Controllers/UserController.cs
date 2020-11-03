using AutoMapper;
using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userDto)
        {
            if (!ModelState.IsValid)
                BadRequest("UserName and Password is required.");

            var user = new User { UserName = userDto.UserName };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var userCreated = await _userManager.FindByNameAsync(user.UserName);

            // Add to Role
            result = await _userManager.AddToRoleAsync(userCreated, UserRolesEnum.User);

            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(userCreated);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            //

            var outcome = _mapper.Map<UserForRegisterDto>(userCreated);

            outcome.Password = userDto.Password;

            return Ok(new { Account = outcome, Token = await _jwtService.GenerateJSONWebToken(userCreated)});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForRegisterDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("UserName and Password is required.");

            var user = await _userManager.FindByNameAsync(userDto.UserName);

            if (user == null)
                return NotFound();

            if (user.UserName != userDto.UserName)
                return Unauthorized();

            var signInResult = await _signInManager.PasswordSignInAsync(user, userDto.Password, false, false);

            if (!signInResult.Succeeded)
                return Unauthorized(signInResult);

            return Ok(new { Token = await _jwtService.GenerateJSONWebToken(user)});
        }
    }
}