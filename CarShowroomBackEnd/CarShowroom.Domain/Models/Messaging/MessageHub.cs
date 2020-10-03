using CarShowroom.Domain.Models.DTO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Models.Messaging
{
    public class MessageHub : Hub
    {
        public async Task SendPrivateMessage(string senderId, MessagePostDto message)
        {
            await Clients.User(message.ReceiverId).SendAsync("sendMessage", senderId, message.ReceiverId, message.Text);
        }

        public Task SendPrivateMessage(Claim claim)
        {
            throw new NotImplementedException();
        }
    }
}
