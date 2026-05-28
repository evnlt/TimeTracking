using TimeTracker.MessageQueue.Settings;

namespace TimeTracker.API.Settings;

public class GlobalSettings
{
    public ConnectionStringSettings ConnectionStrings { get; set; } = default!;
    public RabbitMqSettings RabbitMqSettings { get; set; } = default!;
}