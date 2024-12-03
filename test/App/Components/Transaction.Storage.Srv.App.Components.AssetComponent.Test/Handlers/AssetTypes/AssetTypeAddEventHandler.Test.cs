using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Test.Handlers.AssetTypes;
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

        var handler = new AssetTypeAddEventHandler(
                                            global_sp.GetRequiredService<IRepositoryBase<AssetType>>(),
                                            global_sp.GetRequiredService<IEntityFactory<AssetTypeAddEvent, AssetType>>(),
                                            Output.BuildLoggerFor<AssetTypeAddEventHandler>());
        var request = new AssetTypeAddEvent
        {
            Name = "Test AssetType",
            IsInflationProtected = true,
            IsUnderManagement = false
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

        var savedEntity = await global_sp.GetRequiredService<IReadRepositoryBase<AssetType>>().ListAsync(cancellationToken);
        Assert.Single(savedEntity);

        var assertedEntity = savedEntity.First();
        Assert.Equal(request.Name, assertedEntity.Name);
        Assert.Equal(request.IsInflationProtected, assertedEntity.IsInflationProtected);
        Assert.Equal(request.IsUnderManagement, assertedEntity.IsUnderManagement);

        #endregion
    }
}
