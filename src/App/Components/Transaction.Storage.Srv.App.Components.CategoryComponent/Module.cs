using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Events;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Models;
using Transaction.Storage.Srv.Shared.Events.Interfaces;

namespace Transaction.Storage.Srv.App.Components.CategoryComponent;

public static class Module
{
  public static IServiceCollection Register(this IServiceCollection sc)
  {
    sc.AddScoped<IEntityFactory<CategoryAddEvent, Category>, Category.Factory>();
    sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    return sc;
  }
}
