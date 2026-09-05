using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Application.Features.Tags.Commands.CreateTag;
using PersonalFinanceApp.Application.Features.Tags.Commands.UpdateTag;
using PersonalFinanceApp.Application.Features.Tags.Commands.DeleteTag;
using PersonalFinanceApp.Application.Features.Tags.Queries.GetTags;

namespace PersonalFinanceApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ISender _sender;

    public TagsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _sender.Send(new GetTagsQuery());
        return Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand command)
    {
        var tagId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetTags), new { id = tagId }, tagId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(Guid id, [FromBody] UpdateTagCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Tag ID mismatch.");
        }

        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(Guid id)
    {
        await _sender.Send(new DeleteTagCommand(id));
        return NoContent();
    }
}
