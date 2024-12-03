using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Test.Mocks;
using Transaction.Storage.Srv.App.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Test.AssetAggregate;
public class AssetType_Delete_Test : BaseDbTest<AssetType_Delete_Test>
{
  public AssetType_Delete_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Application.InitApp, logLevel)
  {
  }

  [Fact]
  public async void WHEN_delete_asset_Type_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    AssetTypeDto mockAssetType;
    using (var array_scope = global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAssetType = await AssetTypeMockFactory.Build(sp);
    }

    var usedEvent = new AssetTypeDeleteEvent()
    {
      Id = mockAssetType.Id
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
    Assert.Equal(mockAssetType.Name, assertedResult.Value.Name);
    Assert.Equal(mockAssetType.IsInflationProtected, assertedResult.Value.IsInflationProtected);
    Assert.Equal(mockAssetType.IsUnderManagement, assertedResult.Value.IsUnderManagement);

    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<AssetType>>();
      var assertedEntity = await readRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.Null(assertedEntity);

    }
    #endregion
  }

  [Fact]
  public async Task WHEN_asset_exist_and_no_force_THEN_failAsync()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    AssetTypeDto mockAssetType;
    using (var array_scope = global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAssetType = await AssetTypeMockFactory.Build(sp);
    }

    AssetDto mockAsset;
    using (var array_scope = global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAsset = await AssetMockFactory.Build(sp, mockAssetType);
    }

    var usedEvent = new AssetTypeDeleteEvent()
    {
      Id = mockAssetType.Id
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

    Assert.False(assertedResult.IsSuccess);
    Assert.Equal(ResultStatus.Conflict, assertedResult.Status);

    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<AssetType>>();
      var assertedEntity = await readRep.GetByIdAsync(usedEvent.Id);

      Assert.NotNull(assertedEntity);
      Assert.Equal(mockAssetType.Name, assertedEntity.Name);
      Assert.Equal(mockAssetType.IsInflationProtected, assertedEntity.IsInflationProtected);
      Assert.Equal(mockAssetType.IsUnderManagement, assertedEntity.IsUnderManagement);

    }
    #endregion
  }

  [Fact]
  public async Task WHEN_asset_exist_and_force_THEN_successAsync()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    AssetTypeDto mockAssetType;
    using (var array_scope = global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAssetType = await AssetTypeMockFactory.Build(sp);
    }

    AssetDto mockAsset;
    using (var array_scope = global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAsset = await AssetMockFactory.Build(sp, mockAssetType);
    }

    var usedEvent = new AssetTypeDeleteEvent()
    {
      Id = mockAssetType.Id,
      IsForced = true
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
    Assert.Equal(mockAssetType.Name, assertedResult.Value.Name);
    Assert.Equal(mockAssetType.IsInflationProtected, assertedResult.Value.IsInflationProtected);
    Assert.Equal(mockAssetType.IsUnderManagement, assertedResult.Value.IsUnderManagement);

    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readAssetTypeRep = sp.GetRequiredService<IReadRepositoryBase<AssetType>>();
      var assertedEntity = await readAssetTypeRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.Null(assertedEntity);

      var readAssetRep = sp.GetRequiredService<IReadRepositoryBase<Asset>>();
      var assertedAssetEntity = await readAssetTypeRep.GetByIdAsync(mockAsset.Id);

      Assert.Null(assertedAssetEntity);
    }
    #endregion
  }
}