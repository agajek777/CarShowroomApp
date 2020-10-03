using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageHub _messageHub;

        public MessageController(MessageHub messageHub)
        {
            _messageHub = messageHub;
        }

        [HttpPost]
        public async Task<IActionResult> SendPrivateMessage(MessagePostDto message)
        {
            await _messageHub.SendPrivateMessage(HttpContext.User.FindFirstValue(ClaimTypes.Sid), message);
            return NoContent();
        }
    }
}