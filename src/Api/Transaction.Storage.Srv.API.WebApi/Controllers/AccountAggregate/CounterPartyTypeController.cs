using Ardalis.Specification;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.API.WebApi.Controllers.AccountComponent;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.AccountAggregate;

[ApiController]
[Route($"api/{AccountSwaggerDocInit.ComponentName}/[controller]")]
[ApiExplorerSettings(GroupName = AccountSwaggerDocInit.ComponentName)]
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
    return Ok(ent.Select(e => e.Adapt<AssetTypeDto>()));
  }
}