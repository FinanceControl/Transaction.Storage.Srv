using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate;

public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc)
  {
    sc.AddScoped<IEntityFactory<AccountAddEvent, Account>, Account.Factory>();
    sc.AddScoped<IEntityFactory<CounterPartyAddEvent, CounterParty>, CounterParty.Factory>();
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}