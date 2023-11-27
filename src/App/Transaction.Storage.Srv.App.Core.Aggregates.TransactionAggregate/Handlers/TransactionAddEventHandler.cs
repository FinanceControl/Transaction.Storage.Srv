using Ardalis.Specification;
using Mapster;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Handlers;

public class TransactionAddEventHandler : EntityAddEventHandler<TransactionAddEvent, Header, TransactionDto>
{

  public TransactionAddEventHandler(IRepositoryBase<Header> headerRep,
                                  IEntityFactory<TransactionAddEvent, Header> entityFactory) : base(headerRep, entityFactory)
  {
  }

  protected override Task<TransactionDto> ToResult(Header entity)
  {
    return Task.FromResult(new TransactionDto()
    {
      Header = entity.Adapt<HeaderDto>(),
      Positions = entity.Positions.Select(p => p.Adapt<PositionDto>()).ToList()
    });
  }
}