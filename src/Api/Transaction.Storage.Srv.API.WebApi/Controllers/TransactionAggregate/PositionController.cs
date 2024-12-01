using Ardalis.Specification;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Entities;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.TransactionAggregate;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.TransactionAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.TransactionAggregate)]
public class PositionController : ControllerBase
{
  private readonly IReadRepositoryBase<Position> readRepository;

  public PositionController(IReadRepositoryBase<Position> readRepository)
  {
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(PositionDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<PositionDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<HeaderDto>());
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<PositionDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<PositionDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<PositionDto>()));
  }
}