using TimeTracker.BLL.Exceptions;
using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.BLL.Validators;

// TODO - create an interface?
public class CardValidator
{
    private readonly IUserStore _userStore;
    private readonly ICardStore _cardStore;

    public CardValidator(IUserStore userStore, ICardStore cardStore)
    {
        _userStore = userStore;
        _cardStore = cardStore;
    }
    
    public Result Validate(TouchCardModel model)
    {
        if (model == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull, ErrorType.Validation);
        }

        if (string.IsNullOrWhiteSpace(model.CardUid))
        {
            return Result.Fail(ErrorMessages.CardUidRequired, ErrorType.Validation);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(AssignUserModel model)
    {
        if (model == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull, ErrorType.Validation);
        }

        if (string.IsNullOrWhiteSpace(model.CardUid))
        {
            return Result.Fail(ErrorMessages.CardUidRequired, ErrorType.Validation);
        }
        
        var cardExists = await _cardStore.DoesExist(model.CardUid);
        if (cardExists)
        {
            return Result.Fail(ErrorMessages.CardNotFound, ErrorType.NotFound);
        }

        var userExists = await _userStore.DoesExist(model.UserId);
        if (!userExists)
        {
            return Result.Fail(ErrorMessages.UserNotFound, ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(ListByUserModel model)
    {
        if (model == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull, ErrorType.Validation);
        }

        var userExists = await _userStore.DoesExist(model.UserId);
        if (!userExists)
        {
            return Result.Fail(ErrorMessages.UserNotFound, ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(DeleteCardModel model)
    {
        if (model == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull, ErrorType.Validation);
        }

        var cardExists = await _cardStore.DoesExist(model.CardUid);
        if (cardExists)
        {
            return Result.Fail(ErrorMessages.CardNotFound, ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(DeleteAllCardsByUserModel model)
    {
        if (model == null)
        {
            return Result.Fail(ErrorMessages.ModelIsNull, ErrorType.Validation);
        }

        return Result.Ok();
    }
}