using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.AssetAggregate;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.AssetAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.AssetAggregate)]
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
  public async Task<ActionResult<AssetDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<AssetDto>());
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(typeof(AssetDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AssetDto>> Delete([FromRoute] int id, CancellationToken cancellationToken = new())
  {
    var eventDto = new AssetDeleteEvent() { Id = id };
    var result = await mediator.Send(eventDto, cancellationToken);
    return result.ToActionResult(this);
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<AssetDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<AssetDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<AssetDto>()));
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