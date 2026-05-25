using Microsoft.AspNetCore.Mvc;
using TimeTracker.API.Extensions;
using TimeTracker.API.Extensions.Models;
using TimeTracker.API.Models.Cards;
using TimeTracker.BLL.Abstraction;

namespace TimeTracker.API.Controllers;

[ApiController]
[Route("card")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpPost("touch")]
    public async Task<ActionResult<TouchCardResponse>> Touch(
        [FromBody] TouchCardRequest request)
    {
        var model = request.ToModel();
        var result = await _cardService.Touch(model);

        return result.ToActionResult();
    }

    // TODO - maybe put these endpoints into some AdminCardController?
    [HttpPost("assign")]
    public async Task<ActionResult<AssignUserResponse>> Assign(
        [FromBody] AssignUserRequest request)
    {
        var model = request.ToModel();
        var result = await _cardService.AssignUser(model);

        return result.ToActionResult();
    }

    [HttpPost("list_by_user")]
    public async Task<ActionResult<ListUserCardsResponse>> ListByUser(
        [FromBody] ListUserRequest request)
    {
        var model = request.ToModel();
        var result = await _cardService.ListByUser(model);

        var resultResponse = result
            .Map(models => models.ToResponse(request.UserId));

        return resultResponse.ToActionResult();
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<DeleteCardResponse>> Delete(
        [FromBody] DeleteCardRequest request)
    {
        var model = request.ToModel();
        var result = await _cardService.Delete(model);

        return result.ToActionResult();
    }

    [HttpDelete("delete_all_by_user")]
    public async Task<ActionResult<DeleteAllUserCardsResponse>> DeleteAllByUser(
        [FromBody] DeleteAllCardsByUserRequest request)
    {
        var model = request.ToModel();
        var result = await _cardService.DeleteAllByUser(model);

        return result.ToActionResult();
    }
}