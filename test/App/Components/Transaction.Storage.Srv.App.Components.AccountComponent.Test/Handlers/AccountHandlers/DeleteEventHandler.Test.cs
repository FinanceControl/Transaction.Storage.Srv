using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.AccountHandlers;
using Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Handlers.AccountHandlers;
public class DeleteEventHandler_Test : BaseDbTest<DeleteEventHandler_Test>
{
    public DeleteEventHandler_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Module.Register, logLevel)
    {
        
    }

    [Fact]
    public async Task WHEN_handle_delete_THEN_DeleteFromDatabaseAsync()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");
        
        var existCounterPartyId = (await new CounterPartyMocks(global_sp).AddAsync()).Id;
        var accountMocks = new AccountMocks(global_sp);
        var existAccount1 = await accountMocks.AddAsync("Exist 1",existCounterPartyId);
        var existAccount2 = await accountMocks.AddAsync("Exist 2",existCounterPartyId);
        
        var handler = new AccountDeleteEventHandler(global_sp.GetRequiredService<IRepositoryBase<Account>>());

        var request = new AccountDeleteEvent
        {
            Id= existAccount1.Id
        };

        var cancellationToken = CancellationToken.None;
        #endregion


        #region Act
        Logger.LogDebug("Test ACT");
        var result = await handler.Handle(request, cancellationToken);

        #endregion


        #region Assert
        Logger.LogDebug("Test ASSERT");

        Assert.True(result.IsSuccess);

        var savedEntity = await global_sp.GetRequiredService<IReadRepositoryBase<Account>>().ListAsync(cancellationToken);
        Assert.Single(savedEntity);

        var counterParty = savedEntity.First();
        Assert.Equal("Exist 2", counterParty.Name);
        Assert.Equal(existAccount2.Id, counterParty.Id);

        #endregion
    }

    //TODO: add test when account has dependency you cannot delete without force
}
