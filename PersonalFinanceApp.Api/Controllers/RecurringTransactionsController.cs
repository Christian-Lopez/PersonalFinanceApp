using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Application.Features.RecurringTransactions.Commands;
using PersonalFinanceApp.Application.Features.RecurringTransactions.Queries;

namespace PersonalFinanceApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecurringTransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecurringTransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<RecurringTransactionDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetRecurringTransactionsQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateRecurringTransactionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Cancel(Guid id)
    {
        await _mediator.Send(new CancelRecurringTransactionCommand(id));
        return NoContent();
    }

    [HttpPost("process")]
    public async Task<ActionResult<int>> ProcessDueTransactions()
    {
        var result = await _mediator.Send(new ProcessRecurringTransactionsCommand());
        return Ok(new { ProcessedCount = result });
    }
}
