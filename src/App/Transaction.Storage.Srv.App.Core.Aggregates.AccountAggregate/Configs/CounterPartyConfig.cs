using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Configs;
public class CounterPartyConfig : IEntityTypeConfiguration<CounterParty>
{
  public void Configure(EntityTypeBuilder<CounterParty> entityBuilder)
  {
    entityBuilder
      .HasOne(e=>e.CounterPartyType)
      .WithMany(e=>e.CounterParties)
      .HasForeignKey(e=>e.CounterPartyTypeId)
      .IsRequired();
  }
}