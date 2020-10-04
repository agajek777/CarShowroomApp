using CarShowroom.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.Domain.Interfaces
{
    public interface IMessageHub
    {
        Task SendPrivateMessage(string senderId, MessagePostDto message);
    }
}
