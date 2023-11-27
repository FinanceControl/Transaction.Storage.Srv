using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.TransactionAggregate;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.TransactionAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.TransactionAggregate)]
public class HeaderController : ControllerBase
{
  private readonly IReadRepositoryBase<Header> readRepository;

  public HeaderController(IMediator mediator, IReadRepositoryBase<Header> readRepository)
  {
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(HeaderDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<HeaderDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<HeaderDto>());
  }
  
  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<HeaderDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<HeaderDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<HeaderDto>()));
  }
}