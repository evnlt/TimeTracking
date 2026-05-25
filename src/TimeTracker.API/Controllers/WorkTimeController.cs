using Microsoft.AspNetCore.Mvc;
using TimeTracker.API.Extensions;
using TimeTracker.API.Extensions.Models;
using TimeTracker.API.Models.Worktime;
using TimeTracker.BLL.Abstraction;

namespace TimeTracker.API.Controllers;

[ApiController]
[Route("work_time")]
public class WorkTimeController: ControllerBase
{
    private readonly IWorkTimeService _workTimeService;

    public WorkTimeController(IWorkTimeService workTimeService)
    {
        _workTimeService = workTimeService;
    }

    [HttpPost("set")]
    public async Task<ActionResult> Set([FromBody] SetWorkScheduleRequest request)
    {
        var model = request.ToModel();
        var result = await _workTimeService.Set(model);
        
        return result.ToActionResult();
    }

    [HttpPost("get")]
    public async Task<ActionResult<WorkScheduleResponse>> Get([FromBody] int userId)
    {
        var result = await _workTimeService.Get(userId);
        
        var resultResponse = result
            .Map(models => models.ToResponse());

        return resultResponse.ToActionResult();
    }

    [HttpPost("add_exclusion")]
    public async Task<ActionResult> AddExclusion([FromBody] SetWorkExclusionRequest request)
    {
        var model = request.ToModel();
        var result = await _workTimeService.AddExclusion(model);
        
        return result.ToActionResult();
    }

    [HttpPost("get_exclusion")]
    public async Task<ActionResult<WorkExclusionResponse[]>> GetExclusion([FromBody] int userId)
    {
        var result = await _workTimeService.GetExclusions(userId);
        
        var resultResponse = result
            .Map(models => models.Select(_ => _.ToResponse()).ToArray());

        return resultResponse.ToActionResult();
    }
}