using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;
using Transaction.Storage.Srv.App.Components.AssetComponent.Test.Mocks;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Events;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Test.Handlers;
public class AddEventHandler_Test : BaseDbTest<AddEventHandler_Test>
{
    private static IEnumerable<Func<IServiceCollection, IServiceCollection>> sc_arr =[ 
        AccountComponent.Module.Register ,
        AssetComponent.Module.Register,
        CategoryComponent.Module.Register,
        BudgetComponent.Module.Register,
        Module.Register, 
    ];
    public AddEventHandler_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug)
            : base(output, sc_arr , logLevel)
    {

    }

    [Fact]
    public async Task WHEN_handle_add_THEN_SavesToDatabaseAsync()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");
        var asset = await new AssetMocks(global_sp).AddAsync();
        var account = await new AccountMocks(global_sp).AddAsync();
        var category = await new CategoryMocks(global_sp).AddAsync();
        var budget = await new BudgetMocks(global_sp).AddAsync();

        var handler = new OperationAddEventHandler(
                                            global_sp.GetRequiredService<IRepositoryBase<Operation>>(),
                                            global_sp.GetRequiredService<IEntityFactory<OperationAddEvent, Operation>>(),
                                            Output.BuildLoggerFor<OperationAddEventHandler>());
        var request = new OperationAddEvent
        {
            ExternalId = "ext1",
            PlanDatetime = DateTime.UtcNow,
            CommitDateTime = DateTime.UtcNow + TimeSpan.FromHours(10),
            Description = "description1",
            AccountId = account.Id,
            BudgetId = budget.Id,
            CategoryId = category.Id,
            AssetId = asset.Id,
            Amount = 10,
            Source = "source1",
            Notes = "Note1"
        };

        var cancellationToken = CancellationToken.None;


        #endregion


        #region Act
        Logger.LogDebug("Test ACT");
        var result = await handler.Handle(request, cancellationToken);

        #endregion


        #region Assert
        Logger.LogDebug("Test ASSERT");

        Assert.True(result.IsSuccess);

        var savedEntity = await global_sp.GetRequiredService<IReadRepositoryBase<Operation>>().ListAsync(cancellationToken);
        Assert.Single(savedEntity);

        var assertedEntity = savedEntity.First();
        Assert.Equal(request.ExternalId,assertedEntity.ExternalId);
        Assert.Equal(request.PlanDatetime,assertedEntity.PlanDatetime);
        Assert.Equal(request.CommitDateTime,assertedEntity.CommitDateTime);
        Assert.Equal(request.Description,assertedEntity.Description);
        Assert.Equal(request.AccountId,assertedEntity.AccountId);
        Assert.Equal(request.BudgetId,assertedEntity.BudgetId);
        Assert.Equal(request.CategoryId,assertedEntity.CategoryId);
        Assert.Equal(request.AssetId,assertedEntity.AssetId);
        Assert.Equal(request.Amount,assertedEntity.Amount);
        Assert.Equal(request.Source,assertedEntity.Source);
        Assert.Equal(request.Notes,assertedEntity.Notes);

        #endregion
    }
}