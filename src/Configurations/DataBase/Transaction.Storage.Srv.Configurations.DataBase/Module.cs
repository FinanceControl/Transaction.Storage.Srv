using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Npgsql;
using Microsoft.Extensions.Logging;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using Microsoft.Extensions.Hosting;

namespace Transaction.Storage.Srv.Configurations.DataBase;

public static class Module
{

  private static DbConnectionStringBuilder GetConnectionStringBuilder(IConfiguration? configuration)
  {
    Guard.Against.Null(configuration, message: "IConfiguration is not found in DI");

    var _defConnection = configuration.GetConnectionString("DefaultConnection");
    Guard.Against.Null(_defConnection, message: "DefaultConnection string is not defined");

    var dbSuffix = configuration["ConnectionStrings:DbSuffix"];
    DbConnectionStringBuilder _connectionStringBuilder = new NpgsqlConnectionStringBuilder(_defConnection);
    if (dbSuffix != null)
      _connectionStringBuilder["Database"] += $"_{dbSuffix}";
    return _connectionStringBuilder;
  }

  public static IServiceCollection Register(this IServiceCollection sc)
  {
    using var sp = sc.BuildServiceProvider();
    var logger = sp.GetService<ILogger>();

    sc.AddScoped(typeof(IRepositoryBase<>), typeof(EfRepository<>));
    sc.AddScoped(typeof(IReadRepositoryBase<>), typeof(EfRepository<>));

    sc.AddDbContext<AppDbContext>((provider, builder) =>
      {
        DbConnectionStringBuilder connectionStringSource = GetConnectionStringBuilder(provider.GetService<IConfiguration>());
        var environment = provider.GetService<IHostEnvironment>();

        if (environment != null)
        {
          if (environment.IsDevelopment() || environment.IsEnvironment("Test"))
          {
            builder.EnableSensitiveDataLogging();
            builder.EnableDetailedErrors();
          }
        }

        logger?.LogInformation("PG host: {0}, db: {1}", connectionStringSource["Host"], connectionStringSource["Database"]);
        builder.UseNpgsql(connectionStringSource.ConnectionString);
      });

    logger?.LogInformation("Migration - begin");
    sc.BuildServiceProvider().GetService<AppDbContext>()!.Database.Migrate();
    logger?.LogInformation("Migration - done");
    return sc;
  }

  public static void DeleteDb(this IServiceProvider sp)
  {
    var logger = sp.GetService<ILogger>();
    logger?.LogWarning("Clearup DB");

    var environment = sp.GetService<IHostEnvironment>();
    if (environment == null)
      sp.GetRequiredService<ILogger>().LogWarning("Host environment is not defined, if it is prod FIX THIS");
    else if (environment.IsProduction())
      throw new ApplicationException("Delete Db is not allowed to use in production environment");

    using var dbContext = sp.GetRequiredService<AppDbContext>();
    var result = dbContext.Database.EnsureDeleted();
    logger?.LogWarning("DB clearuped. Result: " + result);
  }
}
