using MediatR;

namespace CarShowroom.Offers.Consumer.Models;

public class DomainMessage<T> : INotification, IRequest
{
    public DomainMessage(T message)
    {
        Message = message;
    }
    
    public T Message { get; set; }
}

public interface IDomainMessage { }
