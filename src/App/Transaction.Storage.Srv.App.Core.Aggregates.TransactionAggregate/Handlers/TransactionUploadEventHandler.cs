using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Entities;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Handlers;

public class TransactionUploadEventHandler : IRequestHandler<TransactionUploadEvent, Result<Dictionary<int, int>>>
{
  private readonly IRepositoryBase<Header> _repository;
  private readonly IOldEntityFactory<TransactionAddEvent, Header> _entityFactory;

  public TransactionUploadEventHandler(IRepositoryBase<Header> entityRep, IOldEntityFactory<TransactionAddEvent, Header> entityFactory)
  {
    this._repository = entityRep;
    this._entityFactory = entityFactory;
  }
  public async Task<Result<Dictionary<int, int>>> Handle(TransactionUploadEvent request, CancellationToken cancellationToken)
  {
    var ValidationErrors = new Dictionary<int, List<ValidationError>>();
    var headers = new List<Header>();
    foreach (var (row, index) in request.records.Select((r, i) => (r, i)))
    {
      var build_result = await _entityFactory.BuildAsync(row, cancellationToken);
      if (!build_result.IsSuccess)
        if (build_result.Status == ResultStatus.Invalid)
          ValidationErrors[index] = build_result.ValidationErrors;
        else
          throw new ApplicationException("Unexpected result mapping");
      else
        headers.Add(build_result.Value);
    }

    // Check validation errors, return invalid response if exist
    if (ValidationErrors.Count > 0)
      return Result.Invalid(
        ValidationErrors
         .SelectMany((kvp) => kvp.Value
           .Select(e => new ValidationError($"[{kvp.Key}].{e.Identifier}", e.ErrorMessage, e.ErrorCode, e.Severity))
           .ToArray())
         .ToArray());

    var new_headers = await _repository.AddRangeAsync(headers, cancellationToken);

    return Result.Success(new_headers.Select((h, idx) => KeyValuePair.Create(idx, h.Id)).ToDictionary());
  }
}