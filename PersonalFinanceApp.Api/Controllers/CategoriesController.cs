using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Application.Features.Categories.Commands.CreateCategory;
using PersonalFinanceApp.Application.Features.Categories.Commands.UpdateCategory;
using PersonalFinanceApp.Application.Features.Categories.Commands.DeleteCategory;
using PersonalFinanceApp.Application.Features.Categories.Queries.GetCategories;

namespace PersonalFinanceApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _sender.Send(new GetCategoriesQuery());
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var categoryId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetCategories), new { id = categoryId }, categoryId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Category ID mismatch.");
        }

        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _sender.Send(new DeleteCategoryCommand(id));
        return NoContent();
    }
}
