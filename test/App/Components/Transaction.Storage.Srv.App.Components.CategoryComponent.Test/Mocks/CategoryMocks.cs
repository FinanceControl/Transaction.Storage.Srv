using Ardalis.GuardClauses;
using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute.Core.Arguments;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Dto;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Events;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;

public class CategoryMocks
{
    static int number = 1;
    IServiceProvider _sp;
    public CategoryMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<CategoryDto> AddAsync(string? name = null)
    {
        if (name == null)
            name = $"Mock Category {number++} {new Random().Next()}";
        var handler = new CategoryAddEventHandler(
                                _sp.GetRequiredService<IRepositoryBase<Category>>(), 
                                _sp.GetRequiredService<IEntityFactory<CategoryAddEvent, Category>>(),
                                _sp.GetRequiredService<ILogger<CategoryAddEventHandler>>());
        var request = new CategoryAddEvent
        {
            Name = name
        };

        var cancellationToken = CancellationToken.None;

        var result = await handler.Handle(request, cancellationToken);
        return result.Value;
    }
}