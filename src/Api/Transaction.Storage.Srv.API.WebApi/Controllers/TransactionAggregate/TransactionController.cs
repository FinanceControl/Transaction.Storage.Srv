using Ardalis.Result.AspNetCore;
using Ardalis.Specification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Specifications;
using CsvHelper;
using System.Globalization;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.TransactionAggregate;

[ApiController]
[Route($"api/{SwaggerGenOptionsInit.TransactionAggregate}/[controller]")]
[ApiExplorerSettings(GroupName = SwaggerGenOptionsInit.TransactionAggregate)]
public class TransactionController : ControllerBase
{
  private readonly IMediator mediator;
  private readonly IReadRepositoryBase<Header> readRepository;

  public TransactionController(IMediator mediator, IReadRepositoryBase<Header> readRepository)
  {
    this.mediator = mediator;
    this.readRepository = readRepository;
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<AssetTypeDto>> Get([FromRoute] int id,
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.SingleOrDefaultAsync(new TransactionByHeaderIdSpec(id), cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<TransactionDto>> Delete([FromRoute] int id, CancellationToken cancellationToken = new())
  {
    var eventDto = new TransactionDeleteEvent() { Id = id };
    var result = await mediator.Send(eventDto, cancellationToken);
    return result.ToActionResult(this);
  }


  [HttpGet()]
  [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll(
      CancellationToken cancellationToken = new())
  {
    var ent = await readRepository.ListAsync(new TransactionByHeaderSpec(), cancellationToken);
    if (ent is null)
      return NotFound();
    return Ok(ent);
  }

  [HttpPost()]
  [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<TransactionDto>> PostAsset([FromBody] TransactionAddEvent transactionAddEvent,
      CancellationToken cancellationToken = new())
  {
    var result = await mediator.Send(transactionAddEvent, cancellationToken);

    if (result.IsSuccess)
    {
      return CreatedAtAction(nameof(Get), new { id = result.Value.Header.Id }, result.Value);
    }

    var res = result.ToActionResult(this);
    return res;
  }

  [HttpPost("upload")]
  public async Task<IActionResult> UploadFile(IFormFile file)
  {
    if (file == null || file.Length == 0)
      return BadRequest("File not selected or empty.");

    try
    {
      using (var reader = new StreamReader(file.OpenReadStream()))
      using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
      {
        // Read the CSV records into a list of dynamic objects
        var records = csv.GetRecords<dynamic>().ToList();

        // Do something with the parsed CSV records (Here, returning the records)
        return Ok(records);
      }
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
    }
  }
}