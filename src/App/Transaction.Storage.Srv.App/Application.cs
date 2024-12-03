using Microsoft.Extensions.DependencyInjection;

namespace Transaction.Storage.Srv.App;

public static class Application
{
  public static IServiceCollection InitApp(this IServiceCollection sc)
  {
    Configurations.DataBase.Module.Register(sc);
    Components.AssetComponent.Module.Register(sc);
    Components.AccountComponent.Module.Register(sc);
    Components.TransactionComponent.Module.Register(sc);
    return sc;
  }
}
