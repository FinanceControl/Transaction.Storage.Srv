using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Events;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Dto;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.CategoryComponent;

[ApiController]
[Route($"api/{CategorySwaggerDocInit.ComponentName}/[controller]")]
[ApiExplorerSettings(GroupName = CategorySwaggerDocInit.ComponentName)]
public class CategoryController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IReadRepositoryBase<Category> readRepository;

  public CategoryController(IMediator mediator, IReadRepositoryBase<Category> readRepository)
  {
    this.mediator = mediator;
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<CategoryDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<CategoryDto>());
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<CategoryDto>()));
  }

  [HttpPost()]
  [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<CategoryDto>> PostCategory([FromBody] CategoryAddEvent newCategoryAddEvent,
      CancellationToken cancellationToken = new())
  {
    var result = await mediator.Send(newCategoryAddEvent, cancellationToken);

    if (result.IsSuccess)
    {
      return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
    }

    var res = result.ToActionResult(this);
    return res;
  }
}