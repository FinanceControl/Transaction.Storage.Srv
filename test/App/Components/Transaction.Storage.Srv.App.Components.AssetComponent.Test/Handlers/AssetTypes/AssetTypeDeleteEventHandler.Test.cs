using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Handlers;
using Transaction.Storage.Srv.App.Components.AssetComponent.Test.Mocks;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Test.Handlers.AssetTypes;
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
        var counterPartyMocks = new AssetTypeMocks(global_sp);
        var existCounterParty1 = await counterPartyMocks.AddAsync("Exist 1");
        var existCounterParty2 = await counterPartyMocks.AddAsync("Exist 2");
        
        var handler = new AssetTypeDeleteEventHandler(global_sp.GetRequiredService<IRepositoryBase<AssetType>>(), global_sp.GetRequiredService<IRepositoryBase<Asset>>());

        var request = new AssetTypeDeleteEvent
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

        var savedEntity = await global_sp.GetRequiredService<IReadRepositoryBase<AssetType>>().ListAsync(cancellationToken);
        Assert.Single(savedEntity);

        var counterParty = savedEntity.First();
        Assert.Equal(existCounterParty2.Name, counterParty.Name);
        Assert.Equal(existCounterParty2.Id, counterParty.Id);

        #endregion
    }

    //todo: add test when counterpary has dependency you cann't delete it
}
