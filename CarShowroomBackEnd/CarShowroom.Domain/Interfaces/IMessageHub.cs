using CarShowroom.Domain.Models.DTO;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface IMessageHub
    {
        Task SendPrivateMessage(string senderId, MessagePostDto message);
    }
}
