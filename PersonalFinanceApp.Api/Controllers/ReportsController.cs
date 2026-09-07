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
    public async Task<IActionResult> GetMonthlySummary([FromQuery] int year, [FromQuery] int month, [FromQuery] Guid? accountId)
    {
        if (year < 2000 || month < 1 || month > 12)
        {
            return BadRequest("Invalid year or month.");
        }

        var summary = await _sender.Send(new GetMonthlySummaryQuery(year, month, accountId));
        return Ok(summary);
    }

    [HttpGet("category-spending")]
    public async Task<IActionResult> GetCategorySpending([FromQuery] int year, [FromQuery] int month, [FromQuery] Guid? accountId)
    {
        if (year < 2000 || month < 1 || month > 12)
        {
            return BadRequest("Invalid year or month.");
        }

        var spending = await _sender.Send(new GetCategorySpendingQuery(year, month, accountId));
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
