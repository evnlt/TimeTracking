using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TimeTracker.MessageQueue.Abstractions;
using TimeTracker.MessageQueue.Settings;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.MessageQueue.Services;

public class CardTouchedConsumer
{
    private ICardTouchEventProcessor _cardTouchEventProcessor;
    private readonly RabbitMqSettings _settings;

    public CardTouchedConsumer(RabbitMqSettings settings, ICardTouchEventProcessor cardTouchEventProcessor)
    {
        _settings = settings;
        _cardTouchEventProcessor = cardTouchEventProcessor;
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

                await _cardTouchEventProcessor.Process(message);
            }
            catch (Exception ex)
            {
                // TODO:
                // logger
                Console.WriteLine(ex);
            }
        };

        await channel.BasicConsumeAsync(
            queue: _settings.CardTouchQueue,
            autoAck: true,
            consumer: consumer);
    }
}