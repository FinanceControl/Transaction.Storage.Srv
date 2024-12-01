using Ardalis.Specification;
using Divergic.Logging.Xunit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.AccountHandlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Test.Mocks;
using Transaction.Storage.Srv.Configurations.DataBase;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Test.Handlers.AccountHandlers;
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
        
        var handler = new DeleteEventHandler(global_sp.GetRequiredService<IRepositoryBase<Account>>(), global_sp.GetRequiredService<IMediator>());

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
}
