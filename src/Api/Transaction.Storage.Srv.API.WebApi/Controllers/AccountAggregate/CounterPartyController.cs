using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;
using Transaction.Storage.Srv.API.WebApi.Controllers.AccountComponent;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.AccountAggregate;

[ApiController]
[Route($"api/{AccountSwaggerDocInit.ComponentName}/[controller]")]
[ApiExplorerSettings(GroupName = AccountSwaggerDocInit.ComponentName)]
public class CounterPartyController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IReadRepositoryBase<CounterParty> readRepository;

  public CounterPartyController(IMediator mediator, IReadRepositoryBase<CounterParty> readRepository)
  {
    this.mediator = mediator;
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(CounterPartyDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<CounterPartyDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return base.Ok(ent.Adapt<CounterPartyDto>());
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<CounterPartyDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<CounterPartyDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<CounterParty>()));
  }

  [HttpPost()]
  [ProducesResponseType(typeof(CounterPartyDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<CounterPartyDto>> PostCounterParty([FromBody] CounterPartyAddEvent newCounterPartyAddEvent,
      CancellationToken cancellationToken = new())
  {
    var result = await mediator.Send(newCounterPartyAddEvent, cancellationToken);

    if (result.IsSuccess)
    {
      return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
    }

    var res = result.ToActionResult(this);
    return res;
  }
}