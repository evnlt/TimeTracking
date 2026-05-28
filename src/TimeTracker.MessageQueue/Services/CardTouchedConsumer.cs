using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TimeTracker.MessageQueue.Abstractions;
using TimeTracker.MessageQueue.Settings;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.MessageQueue.Services;

public class CardTouchedConsumer
{
    private readonly RabbitMqSettings _settings;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<CardTouchedConsumer> _logger;

    public CardTouchedConsumer(RabbitMqSettings settings, IServiceScopeFactory scopeFactory,
        ILogger<CardTouchedConsumer> logger)
    {
        _settings = settings;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task Start()
    {
        // TODO - move this and reuse
        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            Port = _settings.Port,
            UserName = _settings.Username,
            Password = _settings.Password
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _settings.CardTouchQueue,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, args) =>
        {
            try
            {
                var body = args.Body.ToArray();

                var message =
                    JsonSerializer.Deserialize<CardTouchedEventModel>(body);

                if (message == null)
                {
                    return;
                }

                using var scope =
                    _scopeFactory.CreateScope();

                var processor =
                    scope.ServiceProvider
                        .GetRequiredService<ICardTouchEventProcessor>();
                
                await processor.Process(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process CardTouched message");
            }
        };

        await channel.BasicConsumeAsync(
            queue: _settings.CardTouchQueue,
            autoAck: true,
            consumer: consumer);
    }
}