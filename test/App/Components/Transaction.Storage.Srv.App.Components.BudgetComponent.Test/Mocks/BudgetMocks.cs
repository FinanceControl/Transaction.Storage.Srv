using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Dto;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Events;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;

public class BudgetMocks
{
    static int number = 1;
    IServiceProvider _sp;
    public BudgetMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<BudgetDto> AddAsync(string? name = null)
    {
        if (name==null)
            name = $"Mock Budget {number++} {new Random().Next()}";
        var handler = new BudgetAddEventHandler(
                                _sp.GetRequiredService<IRepositoryBase<Budget>>(), 
                                _sp.GetRequiredService<IEntityFactory<BudgetAddEvent, Budget>>(),
                                _sp.GetRequiredService<ILogger<BudgetAddEventHandler>>());
        var request = new BudgetAddEvent
        {
            Name = name
        };

        var cancellationToken = CancellationToken.None;

        var result = await handler.Handle(request, cancellationToken);

        return result.Value;
    }
}