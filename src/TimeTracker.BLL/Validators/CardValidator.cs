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
            return Result.Fail("Model is null", ErrorType.Validation);
        }

        if (string.IsNullOrWhiteSpace(model.CardUid))
        {
            return Result.Fail("CardUid is required", ErrorType.Validation);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(AssignUserModel model)
    {
        if (model == null)
        {
            return Result.Fail("Model is null", ErrorType.Validation);
        }

        if (string.IsNullOrWhiteSpace(model.CardUid))
        {
            return Result.Fail("CardUid is required", ErrorType.Validation);
        }
        
        // TODO - or invert this logic or change it
        // are you supposed to assign with a unique new card uid?
        // so i should check if its already assigned to some user?
        var cardExists = await _cardStore.DoesExist(model.CardUid);
        if (cardExists)
        {
            return Result.Fail("Card does not exist", ErrorType.NotFound);
        }

        var userExists = await _userStore.DoesExist(model.UserId);
        if (!userExists)
        {
            return Result.Fail("User does not exist", ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(ListByUserModel model)
    {
        if (model == null)
        {
            return Result.Fail("Model is null", ErrorType.Validation);
        }

        var userExists = await _userStore.DoesExist(model.UserId);
        if (!userExists)
        {
            return Result.Fail("User does not exist", ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(DeleteCardModel model)
    {
        if (model == null)
        {
            return Result.Fail("Model is null", ErrorType.Validation);
        }

        var cardExists = await _cardStore.DoesExist(model.CardUid);
        if (cardExists)
        {
            return Result.Fail("Card does not exist", ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(DeleteAllCardsByUserModel model)
    {
        if (model == null)
        {
            return Result.Fail("Model is null", ErrorType.Validation);
        }

        // TODO - ???
        /*var cardExists = await _cardStore.DoesExist(model.CardUid);
        if (cardExists)
        {
            return Result.Fail("Card does not exist", ErrorType.NotFound);
        }*/

        return Result.Ok();
    }
}