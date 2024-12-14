using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.AssetAggregate;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.AssetAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.AssetAggregate)]
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
    return Ok(ent.Adapt<AssetTypeDto>());
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<AssetTypeDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<AssetTypeDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<AssetTypeDto>()));
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