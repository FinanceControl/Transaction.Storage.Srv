using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Configs;
public class AccountConfig : IEntityTypeConfiguration<Account>
{
  public void Configure(EntityTypeBuilder<Account> entityBuilder)
  {
    entityBuilder
      .HasOne(e => e.CounterParty)
      .WithMany(e => e.Accounts)
      .HasForeignKey(e => e.CounterPartyId)
      .IsRequired();
    entityBuilder.HasIndex(e => e.Name).IsUnique();
  }
}