using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Test.Mocks;
class AssetTypeMocks
{
    IServiceProvider _sp;
    public AssetTypeMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<AssetTypeDto> AddAsync(string name = "Mock AssetType")
    {
        var handler = new AssetTypeAddEventHandler(
                                            _sp.GetRequiredService<IRepositoryBase<AssetType>>(), 
                                            _sp.GetRequiredService<IEntityFactory<AssetTypeAddEvent, AssetType>>());
        var request = new AssetTypeAddEvent
        {
            Name = name,
            IsInflationProtected = true,
            IsUnderManagement = false
        };

        var cancellationToken = CancellationToken.None;

        var result = await handler.Handle(request, cancellationToken);

        return result.Value;
    }
}