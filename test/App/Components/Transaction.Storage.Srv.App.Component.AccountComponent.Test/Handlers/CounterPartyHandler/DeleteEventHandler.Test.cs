using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.CounterPartyEvents;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.CounterPartyHandlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Test.Mocks;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Test.Handlers.CounterPartyHandlers;
public class DeleteEventHandler_Test : BaseDbTest<DeleteEventHandler_Test>
{

    public DeleteEventHandler_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Module.Register, logLevel)
    {

    }

    [Fact]
    public async Task WHEN_handle_add_THEN_DeleteFromDataBaseAsync()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");
        var counterPartyMocks = new CounterPartyMocks(global_sp);
        var existCounterParty1 = await counterPartyMocks.AddAsync("Exist 1");
        var existCounterParty2 = await counterPartyMocks.AddAsync("Exist 2");
        
        var handler = new CounterPartyDeleteEventHandler(global_sp.GetRequiredService<IRepositoryBase<Account>>(), global_sp.GetRequiredService<IRepositoryBase<CounterParty>>());

        var request = new CounterPartyDeleteEvent
        {
            Id= existCounterParty1.Id
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

        var savedEntity = await global_sp.GetRequiredService<IReadRepositoryBase<CounterParty>>().ListAsync(cancellationToken);
        Assert.Single(savedEntity);

        var counterParty = savedEntity.First();
        Assert.Equal("Exist 2", counterParty.Name);
        Assert.Equal(existCounterParty2.Id, counterParty.Id);

        #endregion
    }

    //todo: add test when counterpary has dependency you cann't delete it
}
