using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate;
public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc)
  {
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}
