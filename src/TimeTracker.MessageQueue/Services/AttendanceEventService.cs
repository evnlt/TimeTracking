using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using TimeTracker.BLL.Abstraction;
using TimeTracker.MessageQueue.Settings;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.MessageQueue.Services;

public class AttendanceEventService : IAttendanceEventService
{
    private readonly RabbitMqSettings _settings;

    public AttendanceEventService(
        RabbitMqSettings settings)
    {
        _settings = settings;
    }

    public async Task PublishCardTouched(CardTouchedEventModel model)
    {
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

        var json = JsonSerializer.Serialize(model);

        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: _settings.CardTouchQueue,
            body: body);
    }
}