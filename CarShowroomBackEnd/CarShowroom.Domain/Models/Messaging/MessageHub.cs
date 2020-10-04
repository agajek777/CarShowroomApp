using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.DTO;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Models.Messaging
{
    public class MessageHub : Hub, IMessageHub
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageHub(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendPrivateMessage(string senderId, MessagePostDto message)
        {
            await _hubContext.Clients.User(message.ReceiverId).SendAsync("sendMessage", senderId, message.ReceiverId, message.Text);
        }
    }
}
