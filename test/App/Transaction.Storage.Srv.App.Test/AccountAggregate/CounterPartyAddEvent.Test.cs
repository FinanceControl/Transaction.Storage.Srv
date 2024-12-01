using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Test.Tools;
using Xunit.Abstractions;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;

namespace Transaction.Storage.Srv.App.Test.AccountAggregate;

public class CounterPartyAddEvent_Test : BaseDbTest<CounterPartyAddEvent_Test>
{

  public CounterPartyAddEvent_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Application.InitApp, logLevel)
  {

  }

  [Fact]
  public async void WHEN_add_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var usedEvent = new CounterPartyAddEvent()
    {
      Name = "Test1",
      CounterPartyTypeId = 1
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

        Result<CounterPartyDto> assertedResult;
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
    Assert.Equal(usedEvent.CounterPartyTypeId, assertedResult.Value.CounterPartyTypeId);

    using (var act_scope = this.global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<Core.Aggregates.AccountAggregate.Entity.CounterParty>>();
      var assertedEntity = await readRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.NotNull(assertedEntity);
      Assert.Equal(usedEvent.Name, assertedEntity.Name);
      Assert.Equal(usedEvent.CounterPartyTypeId, assertedResult.Value.CounterPartyTypeId);

    }
    #endregion
  }
}