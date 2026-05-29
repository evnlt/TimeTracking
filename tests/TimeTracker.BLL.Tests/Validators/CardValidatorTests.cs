using AutoFixture;
using Moq;
using TimeTracker.BLL.Validators;
using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.BLL.Tests.Validators;

public class CardValidatorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IUserStore> _userStoreMock = new();
    private readonly Mock<ICardStore> _cardStoreMock = new();
    private readonly CardValidator _sut;

    public CardValidatorTests()
    {
        _fixture = new Fixture(); //.Customize(new AutoMoqCustomization());

        _sut = new CardValidator(
            _userStoreMock.Object,
            _cardStoreMock.Object);
    }

    [Fact]
    public void Validate_TouchCardModel_Null_ReturnsValidationError()
    {
        var result = _sut.Validate((TouchCardModel)null!);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Validation, result.ErrorType);
    }

    [Fact]
    public void Validate_TouchCardModel_EmptyCardUid_ReturnsValidationError()
    {
        var model = new TouchCardModel
        {
            CardUid = ""
        };

        var result = _sut.Validate(model);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Validation, result.ErrorType);
    }

    [Fact]
    public void Validate_TouchCardModel_Valid_ReturnsSuccess()
    {
        var model = new TouchCardModel
        {
            CardUid = "ABC123"
        };

        var result = _sut.Validate(model);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Validate_AssignUserModel_CardExists_ReturnsNotFound()
    {
        var model = new AssignUserModel
        {
            CardUid = "CARD1",
            UserId = 1
        };

        _cardStoreMock
            .Setup(x => x.DoesExist(model.CardUid))
            .ReturnsAsync(true);

        var result = await _sut.Validate(model);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);
    }

    [Fact]
    public async Task Validate_AssignUserModel_UserDoesNotExist_ReturnsNotFound()
    {
        var model = new AssignUserModel
        {
            CardUid = "CARD1",
            UserId = 99
        };

        _cardStoreMock
            .Setup(x => x.DoesExist(model.CardUid))
            .ReturnsAsync(false);

        _userStoreMock
            .Setup(x => x.DoesExist(model.UserId))
            .ReturnsAsync(false);

        var result = await _sut.Validate(model);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);
    }

    [Fact]
    public async Task Validate_ListByUser_UserDoesNotExist_ReturnsNotFound()
    {
        var model = new ListByUserModel { UserId = 1 };

        _userStoreMock
            .Setup(x => x.DoesExist(model.UserId))
            .ReturnsAsync(false);

        var result = await _sut.Validate(model);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);
    }

    [Fact]
    public async Task Validate_ListByUser_Valid_ReturnsSuccess()
    {
        var model = new ListByUserModel { UserId = 1 };

        _userStoreMock
            .Setup(x => x.DoesExist(model.UserId))
            .ReturnsAsync(true);

        var result = await _sut.Validate(model);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Validate_DeleteCardModel_CardExists_ReturnsNotFound()
    {
        var model = new DeleteCardModel { CardUid = "CARD1" };

        _cardStoreMock
            .Setup(x => x.DoesExist(model.CardUid))
            .ReturnsAsync(true);

        var result = await _sut.Validate(model);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);
    }

    [Fact]
    public async Task Validate_DeleteCardModel_CardDoesNotExist_ReturnsSuccess()
    {
        var model = new DeleteCardModel { CardUid = "CARD1" };

        _cardStoreMock
            .Setup(x => x.DoesExist(model.CardUid))
            .ReturnsAsync(false);

        var result = await _sut.Validate(model);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Validate_DeleteAllCardsByUserModel_NullCheck_ReturnsValidationError()
    {
        var result = await _sut.Validate((DeleteAllCardsByUserModel)null!);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Validation, result.ErrorType);
    }

    [Fact]
    public async Task Validate_DeleteAllCardsByUserModel_Valid_ReturnsSuccess()
    {
        var model = new DeleteAllCardsByUserModel
        {
            UserId = 1
        };

        var result = await _sut.Validate(model);

        Assert.True(result.IsSuccess);
    }
}