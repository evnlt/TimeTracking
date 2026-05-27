using TimeTracker.Models.Models.Cards;

namespace TimeTracker.BLL.Abstraction;

public interface IAttendanceEventService
{
    Task PublishCardTouched(CardTouchedEventModel model);
}