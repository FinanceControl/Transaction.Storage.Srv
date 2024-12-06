using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.CounterPartyHandlers;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Handlers.CounterPartyHandlers;
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

        var handler = new CounterPartyAddEventHandler(
                                        global_sp.GetRequiredService<IRepositoryBase<CounterParty>>(), 
                                        global_sp.GetRequiredService<IEntityFactory<CounterPartyAddEvent, CounterParty>>(),
                                        Output.BuildLoggerFor<CounterPartyAddEventHandler>());
        var request = new CounterPartyAddEvent
        {
            Name = "Test CounterParty",
            CounterPartyTypeId = (int)ICounterPartyType.Enum.Storage
        };

        var cancellationToken = CancellationToken.None;


        #endregion


        #region Act
        Logger.LogDebug("Test ACT");
        var assertedResult = await handler.Handle(request, cancellationToken);

        #endregion


        #region Assert
        Logger.LogDebug("Test ASSERT");

        Assert.True(assertedResult.IsSuccess);  
        Assert.True(assertedResult.Value.Id > 0);

        var savedEntities = await global_sp.GetRequiredService<IReadRepositoryBase<CounterParty>>().ListAsync(cancellationToken);
        Assert.Single(savedEntities);

        var assertedEntity = savedEntities.First();
        Assert.Equal(assertedResult.Value.Id, assertedEntity.Id);
        Assert.Equal(request.Name, assertedEntity.Name);
        Assert.Equal((int)ICounterPartyType.Enum.Storage, assertedEntity.CounterPartyTypeId);

        #endregion
    }
}
