
using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;
using Transaction.Storage.Srv.App.Components.AssetComponent.Test.Mocks;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Dtos;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Events;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Test.Mocks;

public class OperationMocks
{
    static int number = 1;
    IServiceProvider _sp;
    public OperationMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<OperationDto> AddAsync(string? name = null)
    {
        if (name == null)
            name = $"Mock Operation {number++} {new Random().Next()}";
        var asset = await new AssetMocks(_sp).AddAsync();
        var account = await new AccountMocks(_sp).AddAsync();
        var category = await new CategoryMocks(_sp).AddAsync();
        var budget = await new BudgetMocks(_sp).AddAsync();

        var handler = new OperationAddEventHandler(
                                            _sp.GetRequiredService<IRepositoryBase<Operation>>(),
                                            _sp.GetRequiredService<IEntityFactory<OperationAddEvent, Operation>>(),
                                            _sp.GetRequiredService<ILogger<OperationAddEventHandler>>());
        var request = new OperationAddEvent
        {
            ExternalId = "ext1",
            PlanDatetime = DateTime.UtcNow,
            CommitDateTime = DateTime.UtcNow + TimeSpan.FromHours(10),
            Description = name,
            AccountId = account.Id,
            BudgetId = budget.Id,
            CategoryId = category.Id,
            AssetId = asset.Id,
            Amount = 10,
            Source = "source1",
            Notes = "Note1"
        };

        var cancellationToken = CancellationToken.None;
        var result = await handler.Handle(request, cancellationToken);
        return result.Value;
    }
}