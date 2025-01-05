using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.AccountComponent;

public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc,IConfiguration config)
  {
    sc.AddScoped<IEntityFactory<Events.AccountEvents.AccountAddEvent, Account>, Account.Factory>();
    sc.AddScoped<IEntityFactory<Events.CounterPartyEvents.CounterPartyAddEvent, CounterParty>, CounterParty.Factory>();
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}