using Microsoft.AspNetCore.Mvc;
using TimeTracker.API.Extensions;
using TimeTracker.API.Extensions.Models;
using TimeTracker.API.Models.Worktime;
using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Services;

namespace TimeTracker.API.Controllers;

[ApiController]
[Route("work_time")]
public class HistoryController : ControllerBase
{
    private readonly IHistoryService _historyService;

    public HistoryController(IHistoryService historyService)
    {
        _historyService = historyService;
    }

    [HttpPost("history_by_user")]
    public async Task<ActionResult<AttendanceHistoryResponse[]>> HistoryByUser([FromBody] int userId)
    {
        var result = await _historyService.GetByUser(userId);
        
        var resultResponse = result
            .Map(models => models.Select(_ => _.ToResponse()).ToArray());

        return resultResponse.ToActionResult();
    }

    [HttpPost("history")]
    public async Task<ActionResult<AttendanceHistoryResponse[]>> History([FromBody] int limit)
    {
        var result = await _historyService.GetAll(limit);
        
        var resultResponse = result
            .Map(models => models.Select(_ => _.ToResponse()).ToArray());

        return resultResponse.ToActionResult();
    }
}