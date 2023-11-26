using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.Configurations.DataBase;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<AssetType> AssetTypes { get; set; }
  public DbSet<Asset> Assets { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}
