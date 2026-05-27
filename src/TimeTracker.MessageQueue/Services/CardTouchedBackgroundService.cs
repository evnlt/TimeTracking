using Microsoft.Extensions.Hosting;

namespace TimeTracker.MessageQueue.Services;

public class CardTouchedBackgroundService
    : BackgroundService
{
    private readonly CardTouchedConsumer _consumer;

    public CardTouchedBackgroundService(
        CardTouchedConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        await _consumer.Start();
    }
}