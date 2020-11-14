using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Models.Messaging
{
    public class MessageHub : Hub, IMessageHub
    {
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly UserManager<User> _userManager;

        public MessageHub(IHubContext<MessageHub> hubContext, UserManager<User> userManager)
        {
            _hubContext = hubContext;
            _userManager = userManager;
        }
        public async Task SendPrivateMessage(string senderId, MessagePostDto message)
        {
            var sender = await _userManager.FindByIdAsync(senderId);
            var receiver = await _userManager.FindByIdAsync(message.ReceiverId);

            await _hubContext.Clients.User(message.ReceiverId).SendAsync("sendMessage", new { receiverId = message.ReceiverId, receiverName = receiver.UserName, senderId = senderId, senderName = sender.UserName, sent = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), text = message.Text });
        }
    }
}
