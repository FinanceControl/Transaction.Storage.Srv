using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.CounterPartyHandlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Test.Mocks;

class CounterPartyMocks
{
    IServiceProvider _sp;
    public CounterPartyMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<CounterPartyDto> AddAsync(string name = "Mock CounterParty")
    {
        var handler = new AddEventHandler(_sp.GetRequiredService<IRepositoryBase<Entity.CounterParty>>(), _sp.GetRequiredService<IEntityFactory<CounterPartyAddEvent, Entity.CounterParty>>());
        var request = new CounterPartyAddEvent
        {
            Name = name,
            CounterPartyTypeId = (int)ICounterPartyType.Enum.Storage
        };

        var cancellationToken = CancellationToken.None;

        var result = await handler.Handle(request, cancellationToken);

        return result.Value;
    }
}