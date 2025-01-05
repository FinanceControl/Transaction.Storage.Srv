using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent;

public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc,IConfiguration config)
  {
    //sc.AddScoped<IOldEntityFactory<IPositionBodyDto, Position>, Position.Factory>();
    //sc.AddScoped<IOldEntityFactory<TransactionAddEvent, Header>, Header.Factory>();
    sc.AddScoped<IEntityFactory<OperationAddEvent, Operation>, Operation.Factory>();
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}
