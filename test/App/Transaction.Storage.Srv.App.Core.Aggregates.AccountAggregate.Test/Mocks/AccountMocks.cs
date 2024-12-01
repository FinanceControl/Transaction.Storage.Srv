using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.AccountEvents;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.AccountHandlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Test.Mocks;

class AccountMocks
{
    IServiceProvider _sp;
    public AccountMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<AccountDto> AddAsync(string name = "Mock Account", int counterPartyId = -1)
    {
        if (counterPartyId == -1){
            counterPartyId = (await new CounterPartyMocks(_sp).AddAsync()).Id;
        }
        var handler = new AccountAddEventHandler(_sp.GetRequiredService<IRepositoryBase<Account>>(), _sp.GetRequiredService<IEntityFactory<AccountAddEvent, Account>>());
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