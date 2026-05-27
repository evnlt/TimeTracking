using Constants.Enums;
using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Utilities;
using TimeTracker.BLL.Validators;
using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.AttendanceRecord;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.BLL.Services;

public class CardService : ICardService
{
    private readonly CardValidator _cardValidator;
    private readonly ICardStore _cardStore;
    private readonly IAttendanceStore _attendanceStore;

    private readonly IAttendanceEventService _attendanceEventService;

    public CardService(
        CardValidator cardValidator,
        ICardStore cardStore,
        IAttendanceStore attendanceStore,
        IAttendanceEventService attendanceEventService)
    {
        _cardValidator = cardValidator;
        _cardStore = cardStore;
        _attendanceStore = attendanceStore;
        _attendanceEventService = attendanceEventService;
    }

    public async Task<Result> Touch(TouchCardModel model)
    {
        var validationResult = _cardValidator.Validate(model);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        var card = await _cardStore.GetByUid(model.CardUid);

        // TODO move this check into the repository
        if (card == null)
        {
            // TODO - put error messages in constants
            return Result.Fail("Card not found", ErrorType.NotFound);
        }

        var userId = card.UserId;
        var now = DateTime.UtcNow;
        var today = DateOnly.FromDateTime(now);

        var lastRecord = await _attendanceStore.GetLastByUser(new GetLastAttendanceRecordModel
        {
            UserId = userId,
            AttendanceDate = today
        });

        AttendanceAction action;

        if (lastRecord == null || lastRecord.CheckOut != null)
        {
            await _attendanceStore.Create(new CreateAttendanceRecordModel
            {
                UserId = userId,
                AttendanceDate = today,
                CheckIn = now,
                CheckOut = null
            });

            action = AttendanceAction.CheckIn;
        }
        else
        {
            lastRecord.CheckOut = now;
            await _attendanceStore.Update(lastRecord);

            action = AttendanceAction.CheckOut;
        }

        /*try
        {
            _publisher.Publish(new CardTouchedEvent
            {
                CardUid = model.CardUid,
                UserId = userId,
                Timestamp = now,
                Action = action
            });
        }
        catch (Exception ex)
        {
            // IMPORTANT: do NOT fail request
            _logger.LogError(ex, "RabbitMQ publish failed for CardTouched");
        }*/

        // TODO - create a mapper
        await _attendanceEventService.PublishCardTouched(new CardTouchedEventModel
        {
            CardUid = model.CardUid,
            UserId = userId,
            Timestamp = now,
            Action = action
        });

        return Result.Ok();
    }

    public async Task<Result> AssignUser(AssignUserModel model)
    {
        var validationResult = await _cardValidator.Validate(model);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        var assignedAt = DateTime.UtcNow;

        await _cardStore.Assign(model, assignedAt);

        return Result.Ok();
    }

    public async Task<Result<CardModel[]>> ListByUser(ListByUserModel model)
    {
        var validationResult = await _cardValidator.Validate(model);
        if (!validationResult.IsSuccess)
        {
            return validationResult.As<CardModel[]>();
        }

        var cards = await _cardStore.GetByUserId(model.UserId);

        var result = Result<CardModel[]>.Ok(cards);

        return result;
    }

    public async Task<Result> Delete(DeleteCardModel model)
    {
        var validationResult = await _cardValidator.Validate(model);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        var card = await _cardStore.GetByUid(model.CardUid);

        if (card == null)
        {
            return Result.Fail("Card not found", ErrorType.NotFound);
        }

        await _cardStore.Delete(card.CardUid);

        return Result.Ok();
    }

    public async Task<Result> DeleteAllByUser(DeleteAllCardsByUserModel model)
    {
        var validationResult = await _cardValidator.Validate(model);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        await _cardStore.DeleteAllByUserId(model.UserId);

        return Result.Ok();
    }
}