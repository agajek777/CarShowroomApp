using MediatR;

namespace CarShowroom.Offers.Application.Commands.Handlers;

public class AddCarCommandHandler : IRequestHandler<AddCarCommand, Guid>
{
    public Task<Guid> Handle(AddCarCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);



        return Guid.NewGuid();
    }
}
