using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

namespace Transaction.Storage.Srv.App.Test.Mocks;
public static class AssetMockFactory
{
  private static int idx = 0;
  private static object Lock = new object();
  public static async Task<AssetDto> Build(IServiceProvider sp)
  {
    return await Build(sp, await AssetTypeMockFactory.Build(sp));
  }

  public static async Task<AssetDto> Build(IServiceProvider sp, AssetTypeDto assetType)
  {
    return await Build(sp, assetType.Id);
  }

  public static async Task<AssetDto> Build(IServiceProvider sp, int assetTypeId)
  {
    int used_idx;
    lock (Lock){
      used_idx = idx++;
    }
    var usedEvent = new AssetAddEvent()
    {
      Name = $"Test{used_idx}_{DateTimeOffset.UtcNow}_{Thread.CurrentThread.ManagedThreadId}",
      DecimalSize = 3,
      AssetTypeId = assetTypeId
    };
    var mediator = sp.GetRequiredService<IMediator>();
    var assertedResult = await mediator.Send(usedEvent);
    Assert.True(assertedResult.IsSuccess);
    return assertedResult.Value;
  }
}