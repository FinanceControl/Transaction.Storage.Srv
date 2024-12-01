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
public class Asset_Delete_Test : BaseDbTest<Asset_Delete_Test>
{
  public Asset_Delete_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Application.InitApp, logLevel)
  {
  }

  [Fact]
  public async void WHEN_delete_asset_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    AssetDto mockEntity;
    using (var array_scope = global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockEntity = await AssetMockFactory.Build(sp);
    }

    var usedEvent = new AssetDeleteEvent()
    {
      Id = mockEntity.Id
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    Result<AssetDto> assertedResult;
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
    Assert.Equal(mockEntity.Name, assertedResult.Value.Name);
    Assert.Equal(mockEntity.DecimalSize, assertedResult.Value.DecimalSize);
    Assert.Equal(mockEntity.AssetTypeId, assertedResult.Value.AssetTypeId);

    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<Asset>>();
      var assertedEntity = await readRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.Null(assertedEntity);

    }
    #endregion
  }
}