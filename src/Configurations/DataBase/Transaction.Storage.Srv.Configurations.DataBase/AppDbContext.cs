using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;
using Transaction.Storage.Srv.Shared.Database.Models;

namespace Transaction.Storage.Srv.Configurations.DataBase;

public partial class AppDbContext : DbContext
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
  public DbSet<Header> Headers { get; set; }
  public DbSet<Position> Positions { get; set; }

  public override int SaveChanges()
  {
    OnBeforeSaving();
    return base.SaveChanges();
  }
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    OnBeforeSaving();
    return await base.SaveChangesAsync(cancellationToken);
  }
  private void OnBeforeSaving()
  {
    var allEntites = ChangeTracker.Entries<DomainEntity>();
    var entityTuples = ChangeTracker.Entries<DomainEntity>()
       .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
       .Select(e => new { state = e.State, entity = e.Entity })
       .ToArray();
    var currentTime = DateTimeOffset.UtcNow;

    foreach (var entityTpl in entityTuples)
    {
      entityTpl.entity.UpdatedDateTime = currentTime;
      if (entityTpl.state == EntityState.Added)
      {
        entityTpl.entity.CreatedDateTime = currentTime;
      }
    }

  }
}
