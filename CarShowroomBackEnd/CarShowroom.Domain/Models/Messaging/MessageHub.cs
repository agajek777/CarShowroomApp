using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Models.Messaging
{
    public class MessageHub : Hub
    {
        //public Task<bool> SendPrivateMessage(string senderId, string receiverId, Message message)
        //{
        //    this.Clients.User(receiverId).SendAsync("sendMessage", message.SenderId, message.ReceiverId);
        //    return true;
        //}
    }
}
