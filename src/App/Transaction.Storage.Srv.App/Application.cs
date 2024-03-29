﻿using Microsoft.Extensions.DependencyInjection;

namespace Transaction.Storage.Srv.App;

public static class Application
{
  public static IServiceCollection InitApp(this IServiceCollection sc)
  {
    Configurations.DataBase.Module.Register(sc);
    Core.Aggregates.AssetAggregate.Module.Register(sc);
    Core.Aggregates.AccountAggregate.Module.Register(sc);
    Core.Aggregates.TransactionAggregate.Module.Register(sc);
    return sc;
  }
}
