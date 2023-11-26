using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;

namespace Transaction.Storage.Srv.App.Test.Mocks;
public static class AccountMockFactory
{
  private static int idx = 0;
  private static object Lock = new object();
  public static async Task<AccountDto> Build(IServiceProvider sp)
  {
    return await Build(sp, await CounterPartyMockFactory.Build(sp));
  }

  public static async Task<AccountDto> Build(IServiceProvider sp, CounterPartyDto counterParty)
  {
    return await Build(sp, counterParty.Id);
  }

  public static async Task<AccountDto> Build(IServiceProvider sp, int counterPartyId)
  {
    int used_idx;
    lock (Lock)
    {
      used_idx = idx++;
    }
    var usedEvent = new AccountAddEvent()
    {
      Name = $"Test{used_idx}_{DateTimeOffset.UtcNow}_{Thread.CurrentThread.ManagedThreadId}",
      Description = $"Test description {Guid.NewGuid()}",
      IsUnderManagement = new Random().Next() % 2 == 0,
      CounterPartyId = counterPartyId
    };
    var mediator = sp.GetRequiredService<IMediator>();
    var assertedResult = await mediator.Send(usedEvent);
    Assert.True(assertedResult.IsSuccess);
    return assertedResult.Value;
  }
}