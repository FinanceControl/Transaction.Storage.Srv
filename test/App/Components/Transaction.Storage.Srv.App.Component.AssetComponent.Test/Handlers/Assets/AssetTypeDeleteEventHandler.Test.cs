using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Test.Mocks;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Test.Handlers.Assets;
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
        var AssetTypeMocks = await new AssetTypeMocks(global_sp).AddAsync();
        var existAsset1 = await new AssetMocks(global_sp).AddAsync(AssetTypeMocks.Id, "Exist 1");
        var existAsset2 = await new AssetMocks(global_sp).AddAsync(AssetTypeMocks.Id,"Exist 2");
        
        var handler = new AssetDeleteEventHandler(global_sp.GetRequiredService<IRepositoryBase<Asset>>());

        var request = new AssetDeleteEvent
        {
            Id= existAsset1.Id
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

        var savedEntity = await global_sp.GetRequiredService<IReadRepositoryBase<Asset>>().ListAsync(cancellationToken);
        Assert.Single(savedEntity);

        var counterParty = savedEntity.First();
        Assert.Equal(existAsset2.Name, counterParty.Name);
        Assert.Equal(existAsset2.Id, counterParty.Id);

        #endregion
    }

    //todo: add test when counterpary has dependency you cann't delete it
}
