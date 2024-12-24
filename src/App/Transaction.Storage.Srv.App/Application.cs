using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Mapster;
using FastExpressionCompiler;
using Transaction.Storage.Srv.Configurations.DataBase;

namespace Transaction.Storage.Srv.App;

public static class Application
{
  public static IServiceCollection InitApp(this IServiceCollection sc, IConfiguration config)
  {
    TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();
    Module.Register(sc, config);
    Components.AssetComponent.Module.Register(sc,config);
    Components.CategoryComponent.Module.Register(sc,config);
    Components.AccountComponent.Module.Register(sc,config);
    Components.BudgetComponent.Module.Register(sc,config);
    Components.TransactionComponent.Module.Register(sc,config);
    return sc;
  }

  public static void StartApp(this WebApplication app)
  {
    if (!app.Environment.IsProduction())
    {
      using (var scope_sp = app.Services.CreateScope())
      {
        var logger = scope_sp.ServiceProvider.GetRequiredService<ILogger<WebApplication>>();
        
        logger.LogInformation("Migration - begin");
        var db_context = scope_sp.ServiceProvider.GetRequiredService<AppDbContext>();
        db_context.Database.Migrate();
        logger.LogInformation("Migration - done");
      }
    }
    //For production
    //1. use in CICD dotnet ef database update --connection "Host=myhost;Port=5432;Username=myuser;Password=mypassword;Database=mydb"
    //2. or if you run in docker create special docker containor with
    // ENTRYPOINT ["dotnet", "YourApp.dll"]
    // CMD ["ef", "database", "update"]

  }

}
