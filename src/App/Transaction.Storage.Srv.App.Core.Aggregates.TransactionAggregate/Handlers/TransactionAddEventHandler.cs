using Ardalis.Result;
using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Handlers;

public class TransactionAddEventHandler : EntityAddEventHandler<TransactionAddEvent, Header, TransactionDto>
{
  private readonly IReadRepositoryBase<Asset> assetRep;

  public TransactionAddEventHandler(IRepositoryBase<Header> headerRep,
                                IEntityFactory<TransactionAddEvent, Header> entityFactory,
                                  IReadRepositoryBase<Asset> assetRep) : base(headerRep, entityFactory)
  {
    this.assetRep = assetRep;
  }

  protected override Task<TransactionDto> ToResult(Header entity, CancellationToken cancellationToken)
  {
    return Task.FromResult(new TransactionDto()
    {
      Header = entity.Adapt<HeaderDto>(),
      Positions = entity.Positions.Select(p => p.Adapt<PositionDto>()).ToList()
    });
  }
}