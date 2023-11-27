using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Events;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Interfaces;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate;

public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc)
  {
    sc.AddScoped<IEntityFactory<IPositionBodyDto, Position>, Position.Factory>();
    sc.AddScoped<IEntityFactory<TransactionAddEvent, Header>, Header.Factory>();
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}
