using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Components.AssetComponent.Events;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.AssetComponent;
public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc)
  {
    sc.AddScoped<IEntityFactory<AssetAddEvent, Asset>, Asset.Factory>();
    sc.AddScoped<IEntityFactory<AssetTypeAddEvent, AssetType>, AssetType.Factory>();
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}
