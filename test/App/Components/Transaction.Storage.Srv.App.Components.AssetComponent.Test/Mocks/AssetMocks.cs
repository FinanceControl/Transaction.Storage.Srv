using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Handlers;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Test.Mocks;
public class AssetMocks
{
    static int number =1;
    IServiceProvider _sp;
    public AssetMocks(IServiceProvider sp)
    {
        _sp = sp;
    }
    public async Task<AssetDto> AddAsync(string? name = null){
        var assetType = await new AssetTypeMocks(_sp).AddAsync();
        return await AddAsync(assetType.Id, name);
    }

    public async Task<AssetDto> AddAsync(int assetTypeId, string? name = null)
    {
        if (name == null)
            name = $"Mock Asset {number++} {new Random().Next()}";
     
        var handler = new AssetAddEventHandler(
                                        _sp.GetRequiredService<IRepositoryBase<Asset>>(), 
                                        _sp.GetRequiredService<IEntityFactory<AssetAddEvent, Asset>>(),
                                        _sp.GetRequiredService<ILogger<AssetAddEventHandler>>());
        var request = new AssetAddEvent
        {
            Name = name,
            AssetTypeId = assetTypeId!
        };

        var cancellationToken = CancellationToken.None;

        var result = await handler.Handle(request, cancellationToken);

        return result.Value;
    }
}