using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Events;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Dtos;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.TransactionComponent;

[ApiController]
[Route($"api/{TransactionSwaggerDocInit.ComponentName}/[controller]")]
[ApiExplorerSettings(GroupName = TransactionSwaggerDocInit.ComponentName)]
public class OperationController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IReadRepositoryBase<Operation> readRepository;

  public OperationController(IMediator mediator, IReadRepositoryBase<Operation> readRepository)
  {
    this.mediator = mediator;
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(OperationDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<OperationDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<OperationDto>());
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<OperationDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<OperationDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<OperationDto>()));
  }

  [HttpPost()]
  [ProducesResponseType(typeof(OperationDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<OperationDto>> PostOperation([FromBody] OperationAddEvent newOperationAddEvent,
      CancellationToken cancellationToken = new())
  {
    var result = await mediator.Send(newOperationAddEvent, cancellationToken);

    if (result.IsSuccess)
    {
      return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
    }

    var res = result.ToActionResult(this);
    return res;
  }
}