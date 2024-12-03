using Ardalis.Specification;
using Transaction.Storage.Srv.Shared.Events.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;
using Microsoft.Extensions.Logging;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.CounterPartyHandlers;

public class CounterPartyAddEventHandler : EntityAddEventHandler<CounterPartyAddEvent, Entity.CounterParty, CounterPartyDto>
{

  public CounterPartyAddEventHandler(IRepositoryBase<Entity.CounterParty> counterPartyRep,
                                     IEntityFactory<CounterPartyAddEvent, Entity.CounterParty> entityFactory,
                                     ILogger<CounterPartyAddEventHandler> logger) :
                                    base(counterPartyRep, entityFactory,logger)
  {
    
  }
}