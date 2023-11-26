using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

namespace Transaction.Storage.Srv.App.Test.Mocks;
public static class CounterPartyMockFactory
{
  private static int idx = 0;
  private static object Lock = new object();
  public static async Task<CounterPartyDto> Build(IServiceProvider sp)
  {
    int used_idx;
    lock (Lock)
    {
      used_idx = idx++;
    }
    var usedEvent = new CounterPartyAddEvent()
    {
      Name = $"Test{used_idx}_{DateTimeOffset.UtcNow}_{Thread.CurrentThread.ManagedThreadId}",
      CounterPartyTypeId = new Random().Next(1,4)
    };
    var mediator = sp.GetRequiredService<IMediator>();
    var assertedResult = await mediator.Send(usedEvent);
    Assert.True(assertedResult.IsSuccess);
    return assertedResult.Value;
  }
}