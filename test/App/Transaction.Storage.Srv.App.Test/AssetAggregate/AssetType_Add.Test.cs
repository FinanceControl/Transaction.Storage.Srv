using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;
using Transaction.Storage.Srv.App.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Test.AssetAggregate;
public class AssetType_Add_Test : BaseDbTest<AssetType_Add_Test>
{

  public AssetType_Add_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Application.InitApp, logLevel)
  {

  }

  [Fact]
  public async void WHEN_Add_asset_Type_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usedEvent = new AssetTypeAddEvent()
    {
      Name = "Test1",
      IsInflationProtected = true,
      IsUnderManagement = false
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    Result<AssetTypeDto> assertedResult;
    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;
      var mediator = sp.GetRequiredService<IMediator>();
      assertedResult = await mediator.Send(usedEvent);
    }

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Assert.True(assertedResult.IsSuccess);
    Assert.Equal(usedEvent.Name, assertedResult.Value.Name);
    Assert.Equal(usedEvent.IsInflationProtected, assertedResult.Value.IsInflationProtected);
    Assert.Equal(usedEvent.IsUnderManagement, assertedResult.Value.IsUnderManagement);

    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<AssetType>>();
      var assertedEntity = await readRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.NotNull(assertedEntity);
      Assert.Equal(usedEvent.Name, assertedEntity.Name);
      Assert.Equal(usedEvent.IsInflationProtected, assertedEntity.IsInflationProtected);
      Assert.Equal(usedEvent.IsUnderManagement, assertedEntity.IsUnderManagement);

    }
    #endregion
  }
}