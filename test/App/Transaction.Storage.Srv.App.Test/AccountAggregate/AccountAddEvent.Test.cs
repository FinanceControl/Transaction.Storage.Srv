using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Test.Mocks;
using Transaction.Storage.Srv.App.Test.Tools;
using Xunit.Abstractions;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.AccountEvents;

namespace Transaction.Storage.Srv.App.Test.AccountAggregate;

public class AccountAddEvent_Test : BaseDbTest<AccountAddEvent_Test>
{

  public AccountAddEvent_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Application.InitApp, logLevel)
  {

  }

  [Fact]
  public async void WHEN_add_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

        CounterPartyDto mockEntity;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockEntity = await CounterPartyMockFactory.Build(sp);
    }

    var usedEvent = new AccountAddEvent()
    {
      Name = "Test1",
      CounterPartyId = mockEntity.Id,
      Description = "TestDescr1",
      IsUnderManagement = true
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    Result<AccountDto> assertedResult;
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
    Assert.Equal(usedEvent.Description, assertedResult.Value.Description);
    Assert.Equal(usedEvent.IsUnderManagement, assertedResult.Value.IsUnderManagement);
    Assert.Equal(usedEvent.CounterPartyId, assertedResult.Value.CounterPartyId);

    using (var act_scope = this.global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<Account>>();
      var assertedEntity = await readRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.NotNull(assertedEntity);
      Assert.Equal(usedEvent.Name, assertedEntity.Name);
      Assert.Equal(usedEvent.IsUnderManagement, assertedResult.Value.IsUnderManagement);
      Assert.Equal(usedEvent.Description, assertedResult.Value.Description);
      Assert.Equal(usedEvent.CounterPartyId, assertedResult.Value.CounterPartyId);

    }
    #endregion
  }
}