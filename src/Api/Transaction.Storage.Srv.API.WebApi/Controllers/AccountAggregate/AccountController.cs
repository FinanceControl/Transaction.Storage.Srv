using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.AccountAggregate;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.AccountAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.AccountAggregate)]
public class AccountController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IReadRepositoryBase<Account> readRepository;

  public AccountController(IMediator mediator, IReadRepositoryBase<Account> readRepository)
  {
    this.mediator = mediator;
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AccountDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.GetByIdAsync(id, cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Adapt<AccountDto>());
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AccountDto>> Delete([FromRoute] int id, [FromQuery] bool isForced = false, CancellationToken cancellationToken = new())
  {
    var eventDto = new AccountDeleteEvent() { Id = id, IsForced = isForced };
    var result = await mediator.Send(eventDto, cancellationToken);
    return result.ToActionResult(this);
  }

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent.Select(e => e.Adapt<AccountDto>()));
  }

  [HttpPost()]
  [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AccountDto>> PostAccount([FromBody] AccountAddEvent newAccountAddEvent,
      CancellationToken cancellationToken = new())
  {
    var result = await mediator.Send(newAccountAddEvent, cancellationToken);

    if (result.IsSuccess)
    {
      return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
    }

    var res = result.ToActionResult(this);
    return res;
  }
}