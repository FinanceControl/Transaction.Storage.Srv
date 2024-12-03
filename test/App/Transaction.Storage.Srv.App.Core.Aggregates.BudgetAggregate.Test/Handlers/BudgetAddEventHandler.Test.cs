using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Test.Handlers;

public class AddEventHandler_Test : BaseDbTest<AddEventHandler_Test>
{

    public AddEventHandler_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) 
            : base(output, Module.Register, logLevel)
    {

    }

    [Fact]
    public async Task WHEN_handle_add_THEN_SavesToDatabaseAsync()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");
        
        var handler = new BudgetAddEventHandler(
                                            global_sp.GetRequiredService<IRepositoryBase<Budget>>(), 
                                            global_sp.GetRequiredService<IEntityFactory<BudgetAddEvent, Budget>>(),
                                            Output.BuildLoggerFor<BudgetAddEventHandler>());
        var request = new BudgetAddEvent
        {
            Name = "Test Budget",
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

        var savedEntity = await global_sp.GetRequiredService<IReadRepositoryBase<Budget>>().ListAsync(cancellationToken);
        Assert.Single(savedEntity);

        var assertedEntity = savedEntity.First();
        Assert.Equal(request.Name, assertedEntity.Name);

        #endregion
    }
}