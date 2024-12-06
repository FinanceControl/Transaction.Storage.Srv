using Ardalis.Specification;
using Castle.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.AccountHandlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;

public class AccountMocks
{
    private int number = 1;
    IServiceProvider _sp;
    public AccountMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<AccountDto> AddAsync(string? name = null){
        var counterParty = await new CounterPartyMocks(_sp).AddAsync();
        return await AddAsync(counterParty.Id,name);
    }
    public async Task<AccountDto> AddAsync(int counterPartyId, string? name = null)
    {
        if (name == null)
            name = $"Mock Account {number++} {new Random().Next()}";

        var handler = new AccountAddEventHandler(
                                _sp.GetRequiredService<IRepositoryBase<Account>>(), 
                                _sp.GetRequiredService<IEntityFactory<AccountAddEvent, Account>>(),
                                _sp.GetRequiredService<ILogger<AccountAddEventHandler>>());
        var request = new AccountAddEvent
        {
            Name = name,
            Description = "Test account description",
            CounterPartyId = counterPartyId,
            IsUnderManagement = false,
            KeepassId = "123"     
        };

        var cancellationToken = CancellationToken.None;

        var result = await handler.Handle(request, cancellationToken);

        return result.Value;
    }
}