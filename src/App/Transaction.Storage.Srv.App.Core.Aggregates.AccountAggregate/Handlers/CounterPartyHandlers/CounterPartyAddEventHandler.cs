using Ardalis.Specification;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.CounterPartyEvents;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.CounterPartyHandlers;

public class CounterPartyAddEventHandler : EntityAddEventHandler<CounterPartyAddEvent, Entity.CounterParty, CounterPartyDto>
{

  public CounterPartyAddEventHandler(IRepositoryBase<Entity.CounterParty> counterPartyRep,
                                     IEntityFactory<CounterPartyAddEvent, Entity.CounterParty> entityFactory) :
                                    base(counterPartyRep, entityFactory)
  {
    
  }
}