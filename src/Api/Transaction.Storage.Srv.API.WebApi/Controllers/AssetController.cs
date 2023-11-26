
using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.API.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IReadRepositoryBase<Asset> readRepository;

  public AssetController(IMediator mediator, IReadRepositoryBase<Asset> readRepository)
  {
    this.mediator = mediator;
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(AssetDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AssetTypeDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<AssetTypeDto>());
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AssetTypeDto>> Delete([FromRoute] int id, CancellationToken cancellationToken = new())
  {
    var eventDto = new AssetDeleteEvent() { Id = id };
    var result = await mediator.Send(eventDto, cancellationToken);
    return result.ToActionResult(this);
  }

  [HttpPost()]
  [ProducesResponseType(typeof(AssetDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AssetDto>> PostAsset([FromBody] AssetAddEvent newAssetAddEvent,
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