namespace TimeTracker.MessageQueue.Settings;

public class RabbitMqSettings
{
    // TODO - remove?
    public Uri ServerAddress { get; set; }
    
    //
    public string Host { get; set; } = default!;
    public int Port { get; set; }

    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;

    public string CardTouchQueue { get; set; } = default!;
}