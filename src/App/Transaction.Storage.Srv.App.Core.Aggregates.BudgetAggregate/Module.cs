using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Events;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate;

public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc)
  {
    sc.AddScoped<IEntityFactory<BudgetAddEvent, Budget>, Budget.Factory>();
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}