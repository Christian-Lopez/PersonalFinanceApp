using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Application.Features.Accounts.Commands.CreateAccount;
using PersonalFinanceApp.Application.Features.Accounts.Queries.GetAccounts;
using Microsoft.AspNetCore.Authorization;

namespace PersonalFinanceApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ISender _sender;

    public AccountsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        var accountId = await _sender.Send(command);
        return CreatedAtAction(nameof(Create), new { id = accountId }, accountId);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var accounts = await _sender.Send(new GetAccountsQuery());
        return Ok(accounts);
    }
}