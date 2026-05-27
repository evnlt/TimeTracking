using Microsoft.AspNetCore.Mvc;
using TimeTracker.API.Extensions;
using TimeTracker.API.Extensions.Models;
using TimeTracker.API.Models.Worktime;
using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Services;

namespace TimeTracker.API.Controllers;

[ApiController]
[Route("work_time")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpPost("statistics_by_user")]
    public async Task<ActionResult<UserStatisticsResponse>> ByUser([FromBody] int userId)
    {
        var result = await _statisticsService.GetByUser(userId);
        
        var resultResponse = result
            .Map(models => models.ToResponse());

        return resultResponse.ToActionResult();
    }

    [HttpPost("statistics")]
    public async Task<ActionResult<UserStatisticsResponse[]>> All([FromBody] int limit) // TODO - implement pagination
    {
        var result = await _statisticsService.GetAll(limit);
        
        var resultResponse = result
            .Map(models => models.Select(_ => _.ToResponse()).ToArray());

        return resultResponse.ToActionResult();
    }
}