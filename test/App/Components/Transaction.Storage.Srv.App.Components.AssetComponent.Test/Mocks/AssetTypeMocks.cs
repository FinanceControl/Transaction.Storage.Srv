using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Test.Mocks;
public class AssetTypeMocks
{
    static int number = 1;
    IServiceProvider _sp;
    public AssetTypeMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<AssetTypeDto> AddAsync(string? name = null)
    {
        if (name==null)
            name = $"Mock AssetType {number++} {new Random().Next()}";

        var handler = new AssetTypeAddEventHandler(
                                            _sp.GetRequiredService<IRepositoryBase<AssetType>>(), 
                                            _sp.GetRequiredService<IEntityFactory<AssetTypeAddEvent, AssetType>>(),
                                            _sp.GetRequiredService<ILogger<AssetTypeAddEventHandler>>());
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