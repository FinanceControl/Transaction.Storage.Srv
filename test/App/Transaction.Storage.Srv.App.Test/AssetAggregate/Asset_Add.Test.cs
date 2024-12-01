using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;
using Transaction.Storage.Srv.App.Test.Mocks;
using Transaction.Storage.Srv.App.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Test.AssetAggregate;
public class Asset_Add_Test : BaseDbTest<Asset_Add_Test>
{

  public Asset_Add_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Application.InitApp, logLevel)
  {

  }

  [Fact]
  public async void WHEN_Add_asset_Type_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    AssetTypeDto mockAssetType;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAssetType = await AssetTypeMockFactory.Build(sp);
    }

    var usedEvent = new AssetAddEvent()
    {
      Name = "Test1",
      DecimalSize = 10,
      AssetTypeId = mockAssetType.Id,
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    Result<AssetDto> assertedResult;
    using (var act_scope = this.global_sp.CreateScope())
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
    Assert.Equal(usedEvent.DecimalSize, assertedResult.Value.DecimalSize);
    Assert.Equal(usedEvent.AssetTypeId, assertedResult.Value.AssetTypeId);

    using (var act_scope = this.global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<Asset>>();
      var assertedEntity = await readRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.NotNull(assertedEntity);
      Assert.Equal(usedEvent.Name, assertedEntity.Name);
      Assert.Equal(usedEvent.DecimalSize, assertedEntity.DecimalSize);
      Assert.Equal(usedEvent.AssetTypeId, assertedEntity.AssetTypeId);

    }
    #endregion
  }
}