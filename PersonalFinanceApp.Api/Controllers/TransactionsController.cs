using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Application.Features.Transactions.Commands.CreateTransaction;

namespace PersonalFinanceApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ISender _sender;

    public TransactionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
    {
        var transactionId = await _sender.Send(command);
        return CreatedAtAction(nameof(CreateTransaction), new { id = transactionId }, transactionId);
    }
}