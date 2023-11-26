
using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

namespace Transaction.Storage.Srv.API.WebApi.Controllers;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.AccountAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.AccountAggregate)]
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
    return Ok(ent.Adapt<CounterPartyTypeDto>());
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(typeof(CounterPartyDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<CounterPartyDto>> Delete([FromRoute] int id, [FromQuery] bool isForced = false, CancellationToken cancellationToken = new())
  {
    var eventDto = new CounterPartyDeleteEvent() { Id = id, IsForced = isForced };
    var result = await mediator.Send(eventDto, cancellationToken);
    return result.ToActionResult(this);
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