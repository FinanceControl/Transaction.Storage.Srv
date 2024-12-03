using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate;

public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc)
  {
    //sc.AddScoped<IOldEntityFactory<IPositionBodyDto, Position>, Position.Factory>();
    //sc.AddScoped<IOldEntityFactory<TransactionAddEvent, Header>, Header.Factory>();
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}
