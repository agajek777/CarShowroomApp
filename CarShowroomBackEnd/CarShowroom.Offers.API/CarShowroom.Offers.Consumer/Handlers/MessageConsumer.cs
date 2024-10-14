using CarShowroom.Offers.Consumer.Handlers.Users;
using CarShowroom.Offers.Consumer.Models;
using MediatR;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace CarShowroom.Offers.Consumer.Handlers;

public class MessageConsumer(IMediator mediator) : BackgroundService
{
    private const string QueueName = "q.test";

    private readonly IMediator _mediator = mediator;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var model = connection.CreateModel();

        var consumer = new EventingBasicConsumer(model);

        consumer.Received += (model, ea) =>
        {
            using MemoryStream ms = new MemoryStream(ea.Body.ToArray());

            var message = JsonSerializer.Deserialize<UserRemovedEventHandler>(ms);

            _mediator.Send(new DomainMessage<UserRemovedEventHandler>(message)).GetAwaiter().GetResult();
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            model.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
        }

        return Task.CompletedTask;
    }
}
