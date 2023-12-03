using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Handlers;

public class TransactionUploadEventHandler : IRequestHandler<TransactionUploadEvent, Result<Dictionary<int, UploadResponseDto>>>
{
  private readonly IRepositoryBase<Header> _repository;
  private readonly IEntityFactory<TransactionUploadEvent.UploadJsonRowDto, Header> _entityFactory;

  public TransactionUploadEventHandler(IRepositoryBase<Header> entityRep, IEntityFactory<TransactionUploadEvent.UploadJsonRowDto, Header> entityFactory)
  {
    this._repository = entityRep;
    this._entityFactory = entityFactory;
  }
  public async Task<Result<Dictionary<int, UploadResponseDto>>> Handle(TransactionUploadEvent request, CancellationToken cancellationToken)
  {
    foreach (var (row, index) in request.records.Select((r, i) => (r, i)))
    {
      var build_result = await _entityFactory.BuildAsync(row, cancellationToken);
      if (!build_result.IsSuccess)
        return build_result.Map<Header, Dictionary<int, UploadResponseDto>>((at) => throw new ApplicationException("Unexpected result mapping"));
    }
    return null;
  }
}