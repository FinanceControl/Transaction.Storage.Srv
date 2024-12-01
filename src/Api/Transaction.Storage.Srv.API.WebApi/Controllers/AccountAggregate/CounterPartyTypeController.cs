using Ardalis.Specification;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.AccountAggregate;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.AccountAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.AccountAggregate)]
public class CounterPartyTypeController : ControllerBase
{
  private readonly IReadRepositoryBase<App.Core.Aggregates.AccountAggregate.Entity.CounterPartyType> readRepository;

  public CounterPartyTypeController(IReadRepositoryBase<App.Core.Aggregates.AccountAggregate.Entity.CounterPartyType> readRepository)
  {
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(CounterPartyTypeDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<CounterPartyTypeDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<CounterPartyTypeDto>());
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<CounterPartyTypeDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<CounterPartyTypeDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<AssetTypeDto>()));
  }
}