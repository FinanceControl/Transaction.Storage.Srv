using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Dto;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Specifications;
using Transaction.Storage.Srv.App.Test.Mocks;
using Transaction.Storage.Srv.App.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Test.AssetAggregate;
public class TransactionAddEvent_Test : BaseDbTest<TransactionAddEvent_Test>
{

  public TransactionAddEvent_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Application.InitApp, logLevel)
  {

  }

  [Fact]
  public async void WHEN_Add_transaction_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    AssetDto mockAsset;
    AssetDto mockAsset2;
    AccountDto mockAccount;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAsset = await AssetMockFactory.Build(sp);
      mockAsset2 = await AssetMockFactory.Build(sp);
      mockAccount = await AccountMockFactory.Build(sp);
    }

    var usedEvent = new TransactionAddEvent()
    {
      Header = new NewHeaderDto()
      {
        Description = "Test Descr",
        CommitDateTime = new DateTimeOffset(2020, 10, 1, 2, 33, 23, TimeSpan.Zero)
      },
      Positions = new List<NewPositionDto>(){
        new NewPositionDto(){
          AccountId = mockAccount.Id,
          Amount = 10.123M,
          AssetId = mockAsset.Id
        },
        new NewPositionDto(){
          Amount = -10,
          AssetId = mockAsset2.Id
        },
      }
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    Result<TransactionDto> assertedResult;
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
    Assert.Equal(usedEvent.Header.Description, assertedResult.Value.Header.Description);
    Assert.Equal(usedEvent.Header.CommitDateTime, assertedResult.Value.Header.CommitDateTime);

    Assert.Equal(usedEvent.Positions.Count, assertedResult.Value.Positions.Count);
    foreach (var pos in usedEvent.Positions)
    {
      Assert.Contains(assertedResult.Value.Positions, (e) => e.AccountId == pos.AccountId && e.Amount == pos.Amount && e.AssetId == pos.AssetId);
    }

    using (var act_scope = this.global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<Header>>();
      var assertedEntity = await readRep.SingleOrDefaultAsync(new TransactionByHeaderIdSpec(assertedResult.Value.Header.Id));

      Assert.NotNull(assertedEntity);
      Assert.Equal(usedEvent.Header.Description, assertedEntity.Header.Description);
      Assert.Equal(usedEvent.Header.CommitDateTime, assertedEntity.Header.CommitDateTime);

      Assert.Equal(usedEvent.Positions.Count, assertedEntity.Positions.Count);
      foreach (var pos in usedEvent.Positions)
      {
        Assert.Contains(assertedEntity.Positions, (e) => e.AccountId == pos.AccountId && e.Amount == pos.Amount && e.AssetId == pos.AssetId);
      }

    }
    #endregion
  }

  [Fact]
  public async void WHEN_Add_transaction_without_commit_date_THEN_it_added_correctry()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    AssetDto mockAsset;
    AssetDto mockAsset2;
    AccountDto mockAccount;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAsset = await AssetMockFactory.Build(sp);
      mockAsset2 = await AssetMockFactory.Build(sp);
      mockAccount = await AccountMockFactory.Build(sp);
    }

    var usedEvent = new TransactionAddEvent()
    {
      Header = new NewHeaderDto()
      {
        Description = "Test Descr"
      },
      Positions = new List<NewPositionDto>(){
        new NewPositionDto(){
          AccountId = mockAccount.Id,
          Amount = 10,
          AssetId = mockAsset.Id
        },
        new NewPositionDto(){
          Amount = -10,
          AssetId = mockAsset2.Id
        },
      }
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    Result<TransactionDto> assertedResult;
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
    Assert.Equal(usedEvent.Header.Description, assertedResult.Value.Header.Description);
    Assert.Null(assertedResult.Value.Header.CommitDateTime);

    Assert.Equal(usedEvent.Positions.Count, assertedResult.Value.Positions.Count);
    foreach (var pos in usedEvent.Positions)
    {
      Assert.Contains(assertedResult.Value.Positions, (e) => e.AccountId == pos.AccountId && e.Amount == pos.Amount && e.AssetId == pos.AssetId);
    }

    using (var act_scope = this.global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;

      var readRep = sp.GetRequiredService<IReadRepositoryBase<Header>>();
      var assertedEntity = await readRep.SingleOrDefaultAsync(new TransactionByHeaderIdSpec(assertedResult.Value.Header.Id));

      Assert.NotNull(assertedEntity);
      Assert.Equal(usedEvent.Header.Description, assertedEntity.Header.Description);
      Assert.Null(assertedEntity.Header.CommitDateTime);

      Assert.Equal(usedEvent.Positions.Count, assertedEntity.Positions.Count);
      foreach (var pos in usedEvent.Positions)
      {
        Assert.Contains(assertedEntity.Positions, (e) => e.AccountId == pos.AccountId && e.Amount == pos.Amount && e.AssetId == pos.AssetId);
      }

    }
    #endregion
  }

  [Fact]
  public async Task WHEN_give_too_long_amount_dto_THEN_get_invalid_result()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    AssetDto mockAsset;
    AssetDto mockAsset2;
    AccountDto mockAccount;
    using (var array_scope = this.global_sp.CreateScope())
    {
      var sp = array_scope.ServiceProvider;
      mockAsset = await AssetMockFactory.Build(sp, decimalSize: 3);
      mockAsset2 = await AssetMockFactory.Build(sp);
      mockAccount = await AccountMockFactory.Build(sp);
    }

    var usedEvent = new TransactionAddEvent()
    {
      Header = new NewHeaderDto()
      {
        Description = "Test Descr",
        CommitDateTime = new DateTimeOffset(2020, 10, 1, 2, 33, 23, TimeSpan.Zero)
      },
      Positions = new List<NewPositionDto>(){
        new NewPositionDto(){
          AccountId = mockAccount.Id,
          Amount = 10.3333M,
          AssetId = mockAsset.Id
        },
        new NewPositionDto(){
          Amount = -10,
          AssetId = mockAsset2.Id
        },
      }
    };

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    Result<TransactionDto> assertedResult;
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
    Assert.Equal(ResultStatus.Invalid, assertedResult.Status);
    var assertedError = Assert.Single(assertedResult.ValidationErrors);
    Logger.LogInformation(assertedError.ToString());

    #endregion
  }
}