using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.Configurations.DataBase;

public class AppDbContext : DbContext
{
  private readonly ILogger<AppDbContext> logger;

  public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options)
  {
    this.logger = logger;
  }

  public DbSet<AssetType> AssetTypes { get; set; }
  public DbSet<Asset> Assets { get; set; }
  public DbSet<Account> Accounts { get; set; }
  public DbSet<CounterParty> CounterParties { get; set; }
  public DbSet<CounterPartyType> CounterPartyTypes { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    AutoUseConfig(modelBuilder);
  }
  private void AutoUseConfig(ModelBuilder modelBuilder)
  {
    logger?.LogInformation("Auto Config entity");
    IEnumerable<Assembly> usingAssemble = getUsingAssemblyList();

    ApplyConfigFromAssemlies(modelBuilder, usingAssemble);
    logger?.LogInformation("Auto Config entity - done");
  }

  private void ApplyConfigFromAssemlies(ModelBuilder modelBuilder, IEnumerable<Assembly> usingAssemble)
  {
    logger?.LogInformation("Apply config in assembly");
    foreach (var assembly in usingAssemble)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
    logger?.LogInformation("Apply config in assembly - done");
  }

  private IEnumerable<Assembly> getUsingAssemblyList()
  {
    logger?.LogInformation("Search assamblies");
    HashSet<Assembly> usingAssemble = new HashSet<Assembly>();
    foreach (var propInfo in this.GetType().GetProperties())
    {
      if (propInfo.PropertyType.IsGenericType
            && propInfo.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
      {
        var assamble = propInfo.PropertyType.GetGenericArguments()[0].Assembly;

        if (!usingAssemble.Contains(assamble))
        {
          usingAssemble.Add(assamble);
        }
      }
    }
    logger?.LogInformation("Search assamblies - done");
    return usingAssemble;
  }
}
