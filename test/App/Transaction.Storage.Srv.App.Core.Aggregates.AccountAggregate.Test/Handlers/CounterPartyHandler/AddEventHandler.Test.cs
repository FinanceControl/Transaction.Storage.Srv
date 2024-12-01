using Ardalis.Specification;
using Divergic.Logging.Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers.CounterPartyHandlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;
using Transaction.Storage.Srv.Configurations.DataBase;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Test.Handlers.CounterPartyHandlers;
public class AddEventHandler_Test : BaseDbTest<AddEventHandler_Test>
{

    public AddEventHandler_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Module.Register, logLevel)
    {

    }

    [Fact]
    public async Task WHEN_handle_add_THEN_SavesToDatabaseAsync()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");

        var handler = new AddEventHandler(global_sp.GetRequiredService<IRepositoryBase<Entity.CounterParty>>(), global_sp.GetRequiredService<IEntityFactory<CounterPartyAddEvent, Entity.CounterParty>>());
        var request = new CounterPartyAddEvent
        {
            Name = "Test CounterParty",
            CounterPartyTypeId = (int)ICounterPartyType.Enum.Storage
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
        Assert.Equal("Test CounterParty", counterParty.Name);
        Assert.Equal((int)ICounterPartyType.Enum.Storage, counterParty.CounterPartyTypeId);

        #endregion
    }
}
