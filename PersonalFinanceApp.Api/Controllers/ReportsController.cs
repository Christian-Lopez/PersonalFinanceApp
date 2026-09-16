using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Application.Features.Reports.Queries.GetMonthlySummary;
using PersonalFinanceApp.Application.Features.Reports.Queries.GetCategorySpending;

namespace PersonalFinanceApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly ISender _sender;

    public ReportsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("monthly-summary")]
    public async Task<IActionResult> GetMonthlySummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] Guid? accountId)
    {
        if (startDate > endDate)
        {
            return BadRequest("Start date must be before end date.");
        }

        var summary = await _sender.Send(new GetMonthlySummaryQuery(startDate, endDate, accountId));
        return Ok(summary);
    }

    [HttpGet("category-spending")]
    public async Task<IActionResult> GetCategorySpending([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] Guid? accountId)
    {
        if (startDate > endDate)
        {
            return BadRequest("Start date must be before end date.");
        }

        var spending = await _sender.Send(new GetCategorySpendingQuery(startDate, endDate, accountId));
        return Ok(spending);
    }

    [HttpGet("trend")]
    public async Task<IActionResult> GetTrend([FromQuery] int months = 6, [FromQuery] Guid? accountId = null)
    {
        if (months < 1 || months > 60)
        {
            return BadRequest("Months must be between 1 and 60.");
        }

        var trend = await _sender.Send(new PersonalFinanceApp.Application.Features.Reports.Queries.GetTrend.GetTrendQuery(months, accountId));
        return Ok(trend);
    }
}
