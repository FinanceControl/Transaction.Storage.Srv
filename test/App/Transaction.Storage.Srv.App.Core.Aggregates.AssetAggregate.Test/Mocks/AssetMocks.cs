using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Dtos;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Test.Mocks;
class AssetMocks
{
    IServiceProvider _sp;
    public AssetMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<AssetDto> AddAsync(int assetTypeId, string name = "Mock Asset")
    {
        var handler = new AssetAddEventHandler(
                                        _sp.GetRequiredService<IRepositoryBase<Asset>>(), 
                                        _sp.GetRequiredService<IEntityFactory<AssetAddEvent, Asset>>());
        var request = new AssetAddEvent
        {
            Name = name,
            AssetTypeId = assetTypeId
        };

        var cancellationToken = CancellationToken.None;

        var result = await handler.Handle(request, cancellationToken);

        return result.Value;
    }
}