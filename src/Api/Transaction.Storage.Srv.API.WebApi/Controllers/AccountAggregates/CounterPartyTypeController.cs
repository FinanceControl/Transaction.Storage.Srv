
using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.API.WebApi.Controllers;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.AccountAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.AccountAggregate)]
public class CounterPartyTypeController : ControllerBase
{
  private readonly IReadRepositoryBase<CounterPartyType> readRepository;

  public CounterPartyTypeController(IReadRepositoryBase<CounterPartyType> readRepository)
  {
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(CounterPartyTypeDto), StatusCodes.Status200OK)]
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
  [ProducesResponseType(typeof(IEnumerable<CounterPartyTypeDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<AssetTypeDto>> Get(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<AssetTypeDto>()));
  }
}