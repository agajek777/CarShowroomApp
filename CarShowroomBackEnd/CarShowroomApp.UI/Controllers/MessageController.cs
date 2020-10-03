using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarShowroom.Application.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Messaging;
using CarShowroom.UI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarShowroom.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandlingFilter]
    public class MessageController : ControllerBase
    {
        private readonly MessageHub _messageHub;
        private readonly UserManager<User> _userManager;
        private readonly IMessageService<MessagePostDto, MessageGetDto> _messageService;

        public MessageController(MessageHub messageHub, UserManager<User> userManager, IMessageService<MessagePostDto, MessageGetDto> messageService)
        {
            _messageHub = messageHub;
            _userManager = userManager;
            _messageService = messageService;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> SendPrivateMessage([FromBody] MessagePostDto message)
        {
            if (await _userManager.FindByIdAsync(message.ReceiverId) == null)
                return BadRequest(new { Message = $"No user with provided ID { message.ReceiverId } has been found." });

            if (!await _messageService.AddAsync(message, User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Conflict(new { Error = "Request unsuccessfull."});

            await _messageHub.SendPrivateMessage(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), message);

            return NoContent();
        }

        [Authorize(Policy = "Moderator")]
        [HttpGet]
        public async Task<IActionResult> GetPrivateMessages([FromQuery] string userId1, [FromQuery] string userId2)
        {
            if (await _userManager.FindByIdAsync(userId1) == null
                   || await _userManager.FindByIdAsync(userId2) == null)
                return BadRequest(new { Message = $"No user with provided ID { userId1 } or { userId2 } has been found." });

            return Ok(await _messageService.GetAllAsync(userId1, userId2));
        }
    }
}