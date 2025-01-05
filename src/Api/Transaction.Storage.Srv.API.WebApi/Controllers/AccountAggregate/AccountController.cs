using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.API.WebApi.Controllers.AccountComponent;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.AccountAggregate;

[ApiController]
[Route($"api/{AccountSwaggerDocInit.ComponentName}/[controller]")]
[ApiExplorerSettings(GroupName = AccountSwaggerDocInit.ComponentName)]
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

  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(cancellationToken);
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
      return CreatedAtAction(nameof(PostAccount), new { id = result.Value.Id }, result.Value);
    }

    var res = result.ToActionResult(this);
    return res;
  }
}