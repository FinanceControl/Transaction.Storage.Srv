using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Entity;

namespace Transaction.Storage.Srv.App.Core.Aggregates.BudgetAggregate.Configs;
public class AccountConfig : IEntityTypeConfiguration<Budget>
{
  public void Configure(EntityTypeBuilder<Budget> entityBuilder)
  {
    entityBuilder.HasIndex(e => e.Name).IsUnique();
  }
}