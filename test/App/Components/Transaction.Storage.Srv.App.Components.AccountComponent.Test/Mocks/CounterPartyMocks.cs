using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.CounterPartyHandlers;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;

public class CounterPartyMocks
{
    static int number = 1;
    IServiceProvider _sp;
    public CounterPartyMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<CounterPartyDto> AddAsync(string? name = null)
    {
        if (name==null)
            name = $"Mock CounterParty {number++} {new Random().Next()}";
        var handler = new CounterPartyAddEventHandler(
                                _sp.GetRequiredService<IRepositoryBase<Entity.CounterParty>>(), 
                                _sp.GetRequiredService<IEntityFactory<CounterPartyAddEvent, Entity.CounterParty>>(),
                                _sp.GetRequiredService<ILogger<CounterPartyAddEventHandler>>());
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