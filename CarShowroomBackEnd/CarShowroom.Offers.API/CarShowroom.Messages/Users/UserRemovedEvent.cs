using MediatR;

namespace CarShowroom.Messages.Users
{
    public class UserRemovedEvent
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
