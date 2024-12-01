using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Test.Mocks;
using Transaction.Storage.Srv.App.Test.Tools;
using Xunit.Abstractions;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events.CounterPartyEvents;

namespace Transaction.Storage.Srv.App.Test.AccountAggregate;
public class CounterPartyDeleteEvent_Test : BaseDbTest<CounterPartyDeleteEvent_Test>
{
  public CounterPartyDeleteEvent_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Application.InitApp, logLevel)
  {
  }

  [Fact]
  public async void WHEN_delete_counter_party_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

        CounterPartyDto mockEntity;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockEntity = await CounterPartyMockFactory.Build(sp);
    }

    var usedEvent = new CounterPartyDeleteEvent()
    {
      Id = mockEntity.Id
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
    Assert.Equal(mockEntity.Name, assertedResult.Value.Name);
    Assert.Equal(mockEntity.CounterPartyTypeId, assertedResult.Value.CounterPartyTypeId);

    using (var act_scope = this.global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<AssetType>>();
      var assertedEntity = await readRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.Null(assertedEntity);

    }
    #endregion
  }

  [Fact]
  public async Task WHEN_account_exist_and_no_force_THEN_failAsync()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

        CounterPartyDto mockEntity;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockEntity = await CounterPartyMockFactory.Build(sp);
    }

    AccountDto mockSubEntity;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockSubEntity = await AccountMockFactory.Build(sp, mockEntity);
    }

    var usedEvent = new CounterPartyDeleteEvent()
    {
      Id = mockEntity.Id
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

    Assert.False(assertedResult.IsSuccess);
    Assert.Equal(ResultStatus.Conflict, assertedResult.Status);

    using (var act_scope = this.global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<Core.Aggregates.AccountAggregate.Entity.CounterParty>>();
      var assertedEntity = await readRep.GetByIdAsync(usedEvent.Id);

      Assert.NotNull(assertedEntity);
      Assert.Equal(mockEntity.Name, assertedEntity.Name);
      Assert.Equal(mockEntity.CounterPartyTypeId, assertedEntity.CounterPartyTypeId);

    }
    #endregion
  }

  [Fact]
  public async Task WHEN_account_exist_and_force_THEN_successAsync()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

        CounterPartyDto mockEntity;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockEntity = await CounterPartyMockFactory.Build(sp);
    }

    AccountDto mockSubEntity;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockSubEntity = await AccountMockFactory.Build(sp, mockEntity);
    }


    var usedEvent = new CounterPartyDeleteEvent()
    {
      Id = mockEntity.Id,
      IsForced = true
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
    Assert.Equal(mockEntity.Name, assertedResult.Value.Name);
    Assert.Equal(mockEntity.CounterPartyTypeId, assertedResult.Value.CounterPartyTypeId);

    using (var act_scope = this.global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readAssetTypeRep = sp.GetRequiredService<IReadRepositoryBase<Core.Aggregates.AccountAggregate.Entity.CounterParty>>();
      var assertedEntity = await readAssetTypeRep.GetByIdAsync(assertedResult.Value.Id);

      Assert.Null(assertedEntity);

      var readSubRep = sp.GetRequiredService<IReadRepositoryBase<Account>>();
      var assertedSubEntity = await readAssetTypeRep.GetByIdAsync(mockSubEntity.Id);

      Assert.Null(assertedSubEntity);
    }
    #endregion
  }
}