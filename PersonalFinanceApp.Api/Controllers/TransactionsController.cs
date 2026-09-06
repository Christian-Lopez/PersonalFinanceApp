using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Application.Features.Transactions.Commands.CreateTransaction;
using PersonalFinanceApp.Application.Features.Transactions.Commands.UpdateTransaction;
using PersonalFinanceApp.Application.Features.Transactions.Commands.DeleteTransaction;
using PersonalFinanceApp.Application.Features.Transactions.Queries.GetTransactions;

using PersonalFinanceApp.Application.Features.Transactions.Commands.CreateTransfer;

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

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetTransactions(Guid accountId)
    {
        var transactions = await _sender.Send(new GetTransactionsQuery(accountId));
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
    {
        var transactionId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetTransactions), new { accountId = command.AccountId }, transactionId);
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferCommand command)
    {
        await _sender.Send(command);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTransactionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Transaction ID mismatch.");
        }

        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        await _sender.Send(new DeleteTransactionCommand(id));
        return NoContent();
    }
}