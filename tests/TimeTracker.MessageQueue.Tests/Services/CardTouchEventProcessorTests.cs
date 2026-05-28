using AutoFixture;
using Constants.Enums;
using Moq;
using TimeTracker.DAL.Abstraction;
using TimeTracker.MessageQueue.Services;
using TimeTracker.Models.Models.AttendanceRecord;
using TimeTracker.Models.Models.Cards;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.MessageQueue.Tests.Services;

public class CardTouchEventProcessorTests
{
    private readonly Fixture _fixture = new();

    private readonly Mock<IUserStatisticsStore> _statsStore = new();
    private readonly Mock<IAttendanceHistoryStore> _historyStore = new();
    private readonly Mock<IWorkScheduleStore> _scheduleStore = new();
    private readonly Mock<IAttendanceStore> _attendanceStore = new();

    private readonly CardTouchEventProcessor _sut;

    public CardTouchEventProcessorTests()
    {
        _sut = new CardTouchEventProcessor(
            _statsStore.Object,
            _historyStore.Object,
            _scheduleStore.Object,
            _attendanceStore.Object);
    }
    
    [Fact]
    public async Task Process_ShouldCreateHistoryRecord()
    {
        // Arrange
        var model = new CardTouchedEventModel
        {
            UserId = 1,
            Timestamp = DateTime.UtcNow,
            Action = AttendanceAction.CheckIn
        };

        // Act
        await _sut.Process(model);

        // Assert
        _historyStore.Verify(x => x.Create(
                It.Is<AttendanceHistoryModel>(h =>
                    h.UserId == model.UserId &&
                    h.Action == model.Action &&
                    h.Timestamp == model.Timestamp)),
            Times.Once);
    }
    
    [Fact]
    public async Task CheckIn_AfterStartTime_ShouldIncreaseLateCounters()
    {
        // Arrange
        _statsStore.Setup(x => x.GetByUser(It.IsAny<int>()))
            .ReturnsAsync(new UserStatisticsModel { UserId = 1 });

        _scheduleStore.Setup(x => x.Get(It.IsAny<int>()))
            .ReturnsAsync(new WorkScheduleModel
            {
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(17, 0),
                FreeSchedule = false
            });

        var model = new CardTouchedEventModel
        {
            UserId = 1,
            Timestamp = new DateTime(2026, 1, 1, 10, 0, 0),
            Action = AttendanceAction.CheckIn
        };

        // Act
        await _sut.Process(model);

        // Assert
        _statsStore.Verify(x => x.Upsert(
                It.Is<UserStatisticsModel>(s =>
                    s.LateCount == 1 &&
                    s.LateWithoutReason == 1)),
            Times.Once);
    }
    
    [Fact]
    public async Task CheckOut_ShouldCalculateWorkedTimeAndEarlyLeave()
    {
        // Arrange
        _statsStore.Setup(x => x.GetByUser(It.IsAny<int>()))
            .ReturnsAsync(new UserStatisticsModel { UserId = 1 });

        _scheduleStore.Setup(x => x.Get(It.IsAny<int>()))
            .ReturnsAsync(new WorkScheduleModel
            {
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(17, 0),
                FreeSchedule = false
            });

        _attendanceStore.Setup(x => x.GetLastByUser(It.IsAny<GetLastAttendanceRecordModel>()))
            .ReturnsAsync(new AttendanceRecordModel
            {
                CheckIn = new DateTime(2026, 1, 1, 9, 0, 0),
                CheckOut = new DateTime(2026, 1, 1, 16, 0, 0)
            });

        var model = new CardTouchedEventModel
        {
            UserId = 1,
            Timestamp = new DateTime(2026, 1, 1, 16, 0, 0),
            Action = AttendanceAction.CheckOut
        };

        // Act
        await _sut.Process(model);

        // Assert
        _statsStore.Verify(x => x.Upsert(
                It.Is<UserStatisticsModel>(s =>
                    s.Worked > TimeSpan.Zero &&
                    s.EarlyLeaveCount == 1 &&
                    s.EarlyWithoutReason == 1)),
            Times.Once);
    }
    
    [Fact]
    public async Task NoSchedule_ShouldJustUpsertStatistics()
    {
        // Arrange
        _statsStore.Setup(x => x.GetByUser(It.IsAny<int>()))
            .ReturnsAsync(new UserStatisticsModel { UserId = 1 });

        _scheduleStore.Setup(x => x.Get(It.IsAny<int>()))
            .ReturnsAsync((WorkScheduleModel?)null);

        var model = new CardTouchedEventModel
        {
            UserId = 1,
            Timestamp = DateTime.UtcNow,
            Action = AttendanceAction.CheckIn
        };

        // Act
        await _sut.Process(model);

        // Assert
        _statsStore.Verify(x => x.Upsert(It.IsAny<UserStatisticsModel>()), Times.Once);
    }
}