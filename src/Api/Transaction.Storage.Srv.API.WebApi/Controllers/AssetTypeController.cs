using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Mappers;

namespace Transaction.Storage.Srv.API.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetTypeController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IReadRepositoryBase<AssetType> readRepository;

  public AssetTypeController(IMediator mediator, IReadRepositoryBase<AssetType> readRepository)
  {
    this.mediator = mediator;
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(AssetTypeDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AssetTypeDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.ToDTO());
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AssetTypeDto>> Delete([FromRoute] int id, [FromQuery] bool isForced = false, CancellationToken cancellationToken = new())
  {
    var eventDto = new AssetTypeDeleteEvent() { Id = id, IsForced = isForced };
    var result = await mediator.Send(eventDto, cancellationToken);
    return result.ToActionResult(this);
  }

  [HttpPost()]
  [ProducesResponseType(typeof(AssetTypeDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AssetTypeDto>> PostAsset([FromBody] AssetTypeAddEvent newAssetAddEvent,
      CancellationToken cancellationToken = new())
  {
    var result = await mediator.Send(newAssetAddEvent, cancellationToken);

    if (result.IsSuccess)
    {
      return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
    }

    var res = result.ToActionResult(this);
    return res;
  }
}