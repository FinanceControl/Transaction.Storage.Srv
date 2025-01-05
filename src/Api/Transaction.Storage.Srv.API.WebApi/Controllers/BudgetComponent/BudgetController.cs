using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Events;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Dto;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.BudgetComponent;

[ApiController]
[Route($"api/{BudgetSwaggerDocInit.ComponentName}/[controller]")]
[ApiExplorerSettings(GroupName = BudgetSwaggerDocInit.ComponentName)]
public class BudgetController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IReadRepositoryBase<Budget> readRepository;

  public BudgetController(IMediator mediator, IReadRepositoryBase<Budget> readRepository)
  {
    this.mediator = mediator;
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<BudgetDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<BudgetDto>());
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<BudgetDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<BudgetDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<BudgetDto>()));
  }

  [HttpPost()]
  [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<BudgetDto>> PostBudget([FromBody] BudgetAddEvent newBudgetAddEvent,
      CancellationToken cancellationToken = new())
  {
    var result = await mediator.Send(newBudgetAddEvent, cancellationToken);

    if (result.IsSuccess)
    {
      return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
    }

    var res = result.ToActionResult(this);
    return res;
  }
}