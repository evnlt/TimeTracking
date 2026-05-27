using TimeTracker.Models.Models.Cards;

namespace TimeTracker.MessageQueue.Abstractions;

public interface ICardTouchEventProcessor
{
    Task Process(CardTouchedEventModel model);
}