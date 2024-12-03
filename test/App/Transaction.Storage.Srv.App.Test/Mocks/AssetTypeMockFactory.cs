using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;

namespace Transaction.Storage.Srv.App.Test.Mocks;
public static class AssetTypeMockFactory
{
  private static int idx = 0;
  private static object Lock = new object();
  public static async Task<AssetTypeDto> Build(IServiceProvider sp)
  {
    int used_idx;
    lock (Lock)
    {
      used_idx = idx++;
    }
    var usedEvent = new AssetTypeAddEvent()
    {
      Name = $"Test{used_idx}_{DateTimeOffset.UtcNow}_{Thread.CurrentThread.ManagedThreadId}",
      IsInflationProtected = true,
      IsUnderManagement = false
    };
    var mediator = sp.GetRequiredService<IMediator>();
    var assertedResult = await mediator.Send(usedEvent);
    Assert.True(assertedResult.IsSuccess);
    return assertedResult.Value;
  }
}