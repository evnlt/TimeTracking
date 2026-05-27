using Constants.Enums;
using TimeTracker.DAL.Abstraction;
using TimeTracker.MessageQueue.Abstractions;
using TimeTracker.Models.Models.Cards;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.MessageQueue.Services;

public class CardTouchEventProcessor : ICardTouchEventProcessor
{
    private readonly IUserStatisticsStore _statisticsStore;
    private readonly IAttendanceHistoryStore _historyStore;

    public CardTouchEventProcessor(
        IUserStatisticsStore statisticsStore,
        IAttendanceHistoryStore historyStore)
    {
        _statisticsStore = statisticsStore;
        _historyStore = historyStore;
    }

    public async Task Process(CardTouchedEventModel model)
    {
        await _historyStore.Create(new AttendanceHistoryModel
        {
            UserId = model.UserId,
            Timestamp = model.Timestamp,
            Action = model.Action
        });

        await UpdateStatistics(model);
    }

    private async Task UpdateStatistics(
        CardTouchedEventModel model)
    {
        var statistics =
            await _statisticsStore.GetByUser(model.UserId);

        if (statistics == null)
        {
            statistics = new UserStatisticsModel
            {
                UserId = model.UserId
            };
        }

        if (model.Action == AttendanceAction.CheckIn)
        {
            // TODO - do something here
            // example:
            // count lateness later
        }

        if (model.Action == AttendanceAction.CheckOut)
        {
            // example:
            // calculate worked time later
        }

        await _statisticsStore.Upsert(statistics);
    }
}