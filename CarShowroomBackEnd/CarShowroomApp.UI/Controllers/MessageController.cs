using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageHub _messageHub;
        private readonly UserManager<User> _userManager;

        public MessageController(MessageHub messageHub, UserManager<User> userManager)
        {
            _messageHub = messageHub;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SendPrivateMessage(MessagePostDto message)
        {
            if (await _userManager.FindByIdAsync(message.ReceiverId) == null)
                return BadRequest(new { Message = $"No user with provided ID { message.ReceiverId } has been found." });



            await _messageHub.SendPrivateMessage(HttpContext.User.FindFirstValue(ClaimTypes.Sid), message);
            return NoContent();
        }
    }
}