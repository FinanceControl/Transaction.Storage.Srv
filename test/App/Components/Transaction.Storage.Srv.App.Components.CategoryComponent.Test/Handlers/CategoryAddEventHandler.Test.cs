using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Events;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent.Test.Handlers;

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
        
        var handler = new CategoryAddEventHandler(
                                            global_sp.GetRequiredService<IRepositoryBase<Category>>(), 
                                            global_sp.GetRequiredService<IEntityFactory<CategoryAddEvent, Category>>(),
                                            Output.BuildLoggerFor<CategoryAddEventHandler>());
        var request = new CategoryAddEvent
        {
            Name = "Test Category",
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

        var savedEntity = await global_sp.GetRequiredService<IReadRepositoryBase<Category>>().ListAsync(cancellationToken);
        Assert.Single(savedEntity);

        var assertedEntity = savedEntity.First();
        Assert.Equal(assertedResult.Value.Id, assertedEntity.Id);
        Assert.Equal(request.Name, assertedEntity.Name);

        #endregion
    }
}