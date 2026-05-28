using Constants.Enums;
using TimeTracker.DAL.Abstraction;
using TimeTracker.MessageQueue.Abstractions;
using TimeTracker.Models.Models.AttendanceRecord;
using TimeTracker.Models.Models.Cards;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.MessageQueue.Services;

public class CardTouchEventProcessor : ICardTouchEventProcessor
{
    private readonly IUserStatisticsStore _statisticsStore;
    private readonly IAttendanceHistoryStore _historyStore;
    private readonly IWorkScheduleStore _workScheduleStore;
    private readonly IAttendanceStore _attendanceStore;

    public CardTouchEventProcessor(
        IUserStatisticsStore statisticsStore,
        IAttendanceHistoryStore historyStore,
        IWorkScheduleStore workScheduleStore, 
        IAttendanceStore attendanceStore)
    {
        _statisticsStore = statisticsStore;
        _historyStore = historyStore;
        _workScheduleStore = workScheduleStore;
        _attendanceStore = attendanceStore;
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

    private async Task UpdateStatistics(CardTouchedEventModel model)
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

        var schedule =
            await _workScheduleStore.Get(model.UserId);

        if (schedule == null)
        {
            await _statisticsStore.Upsert(statistics);
            return;
        }

        // CHECK-IN
        if (model.Action == AttendanceAction.CheckIn)
        {
            var checkInTime = TimeOnly.FromDateTime(model.Timestamp);

            if (!schedule.FreeSchedule &&
                checkInTime > schedule.StartTime)
            {
                statistics.LateCount++;
                statistics.LateWithoutReason++;
            }
        }

        // CHECK-OUT
        if (model.Action == AttendanceAction.CheckOut)
        {
            var today =
                DateOnly.FromDateTime(model.Timestamp);

            var attendance =
                await _attendanceStore.GetLastByUser(
                    new GetLastAttendanceRecordModel
                    {
                        UserId = model.UserId,
                        AttendanceDate = today
                    });

            if (attendance != null &&
                attendance.CheckOut.HasValue)
            {
                var worked =
                    attendance.CheckOut.Value - attendance.CheckIn;

                statistics.Worked += worked;

                var expected =
                    schedule.EndTime - schedule.StartTime;

                statistics.ExpectedWork += expected;

                if (worked < expected)
                {
                    statistics.Missing += expected - worked;

                    statistics.EarlyLeaveCount++;
                    statistics.EarlyWithoutReason++;
                }
            }
        }

        await _statisticsStore.Upsert(statistics);
    }
}