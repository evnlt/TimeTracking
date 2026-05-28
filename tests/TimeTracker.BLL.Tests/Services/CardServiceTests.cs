using AutoFixture;
using Constants.Enums;
using Moq;
using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Services;
using TimeTracker.BLL.Validators;
using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.AttendanceRecord;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.BLL.Tests.Services;

public class CardServiceTests
{
    private readonly Fixture _fixture = new();

    private readonly Mock<ICardStore> _cardStoreMock = new();
    private readonly Mock<IUserStore> _userStoreMock = new();
    private readonly Mock<IAttendanceStore> _attendanceStoreMock = new();
    private readonly Mock<IAttendanceEventService> _attendanceEventServiceMock = new();

    private readonly CardValidator _cardValidator;

    private readonly CardService _service;

    public CardServiceTests()
    {
        _cardValidator = new CardValidator(_userStoreMock.Object, _cardStoreMock.Object);

        _service = new CardService(
            _cardValidator,
            _cardStoreMock.Object,
            _attendanceStoreMock.Object,
            _attendanceEventServiceMock.Object);
    }

    [Fact]
    public async Task Touch_ShouldReturnValidationError_WhenCardUidIsEmpty()
    {
        // Arrange
        var model = new TouchCardModel
        {
            CardUid = string.Empty
        };

        // Act
        var result = await _service.Touch(model);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Validation, result.ErrorType);
    }

    [Fact]
    public async Task Touch_ShouldReturnNotFound_WhenCardDoesNotExist()
    {
        // Arrange
        var model = _fixture.Create<TouchCardModel>();

        _cardStoreMock
            .Setup(x => x.GetByUid(model.CardUid))
            .ReturnsAsync((CardModel?)null);

        // Act
        var result = await _service.Touch(model);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);

        _attendanceStoreMock.Verify(
            x => x.Create(It.IsAny<CreateAttendanceRecordModel>()),
            Times.Never);
    }

    [Fact]
    public async Task Touch_ShouldCreateAttendanceRecord_WhenNoPreviousRecordExists()
    {
        // Arrange
        var model = _fixture.Create<TouchCardModel>();

        var card = new CardModel
        {
            CardUid = model.CardUid,
            UserId = 1
        };

        _cardStoreMock
            .Setup(x => x.GetByUid(model.CardUid))
            .ReturnsAsync(card);

        _attendanceStoreMock
            .Setup(x => x.GetLastByUser(It.IsAny<GetLastAttendanceRecordModel>()))
            .ReturnsAsync((AttendanceRecordModel?)null);

        // Act
        var result = await _service.Touch(model);

        // Assert
        Assert.True(result.IsSuccess);

        _attendanceStoreMock.Verify(
            x => x.Create(It.Is<CreateAttendanceRecordModel>(m =>
                m.UserId == card.UserId &&
                m.CheckOut == null)),
            Times.Once);

        _attendanceStoreMock.Verify(
            x => x.Update(It.IsAny<AttendanceRecordModel>()),
            Times.Never);

        _attendanceEventServiceMock.Verify(
            x => x.PublishCardTouched(It.Is<CardTouchedEventModel>(m =>
                m.Action == AttendanceAction.CheckIn)),
            Times.Once);
    }

    [Fact]
    public async Task Touch_ShouldUpdateAttendanceRecord_WhenOpenRecordExists()
    {
        // Arrange
        var model = _fixture.Create<TouchCardModel>();

        var card = new CardModel
        {
            CardUid = model.CardUid,
            UserId = 1
        };

        var attendanceRecord = new AttendanceRecordModel
        {
            Id = 1,
            UserId = 1,
            AttendanceDate = DateOnly.FromDateTime(DateTime.UtcNow),
            CheckIn = DateTime.UtcNow.AddHours(-8),
            CheckOut = null
        };

        _cardStoreMock
            .Setup(x => x.GetByUid(model.CardUid))
            .ReturnsAsync(card);

        _attendanceStoreMock
            .Setup(x => x.GetLastByUser(It.IsAny<GetLastAttendanceRecordModel>()))
            .ReturnsAsync(attendanceRecord);

        // Act
        var result = await _service.Touch(model);

        // Assert
        Assert.True(result.IsSuccess);

        _attendanceStoreMock.Verify(
            x => x.Update(It.Is<AttendanceRecordModel>(m =>
                m.CheckOut != null)),
            Times.Once);

        _attendanceStoreMock.Verify(
            x => x.Create(It.IsAny<CreateAttendanceRecordModel>()),
            Times.Never);

        _attendanceEventServiceMock.Verify(
            x => x.PublishCardTouched(It.Is<CardTouchedEventModel>(m =>
                m.Action == AttendanceAction.CheckOut)),
            Times.Once);
    }

    [Fact]
    public async Task AssignUser_ShouldAssignCard()
    {
        // Arrange
        var model = _fixture.Create<AssignUserModel>();
        
        _userStoreMock
            .Setup(x => x.DoesExist(model.UserId))
            .ReturnsAsync(true);

        // Act
        var result = await _service.AssignUser(model);

        // Assert
        Assert.True(result.IsSuccess);

        _cardStoreMock.Verify(
            x => x.Assign(
                It.Is<AssignUserModel>(m =>
                    m.UserId == model.UserId &&
                    m.CardUid == model.CardUid),
                It.IsAny<DateTime>()),
            Times.Once);
    }

    [Fact]
    public async Task ListByUser_ShouldReturnCards()
    {
        // Arrange
        var model = _fixture.Create<ListByUserModel>();
        
        _userStoreMock
            .Setup(x => x.DoesExist(model.UserId))
            .ReturnsAsync(true);

        var cards = _fixture.CreateMany<CardModel>(3).ToArray();

        _cardStoreMock
            .Setup(x => x.GetByUserId(model.UserId))
            .ReturnsAsync(cards);

        // Act
        var result = await _service.ListByUser(model);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(3, result.Value.Length);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenCardDoesNotExist()
    {
        // Arrange
        var model = _fixture.Create<DeleteCardModel>();

        _cardStoreMock
            .Setup(x => x.GetByUid(model.CardUid))
            .ReturnsAsync((CardModel?)null);

        // Act
        var result = await _service.Delete(model);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);

        _cardStoreMock.Verify(
            x => x.Delete(It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task Delete_ShouldDeleteCard()
    {
        // Arrange
        var model = _fixture.Create<DeleteCardModel>();

        var card = new CardModel
        {
            CardUid = model.CardUid,
            UserId = 1
        };

        _cardStoreMock
            .Setup(x => x.GetByUid(model.CardUid))
            .ReturnsAsync(card);

        // Act
        var result = await _service.Delete(model);

        // Assert
        Assert.True(result.IsSuccess);

        _cardStoreMock.Verify(
            x => x.Delete(model.CardUid),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAllByUser_ShouldDeleteAllCards()
    {
        // Arrange
        var model = _fixture.Create<DeleteAllCardsByUserModel>();

        // Act
        var result = await _service.DeleteAllByUser(model);

        // Assert
        Assert.True(result.IsSuccess);

        _cardStoreMock.Verify(
            x => x.DeleteAllByUserId(model.UserId),
            Times.Once);
    }
}