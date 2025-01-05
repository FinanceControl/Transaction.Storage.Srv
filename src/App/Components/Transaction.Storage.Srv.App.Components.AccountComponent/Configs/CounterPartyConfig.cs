using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Configs;
public class CounterPartyConfig : IEntityTypeConfiguration<CounterParty>
{
  public void Configure(EntityTypeBuilder<CounterParty> entityBuilder)
  {
    entityBuilder
      .HasOne(e=>e.CounterPartyType)
      .WithMany(e=>e.CounterParties)
      .HasForeignKey(e=>e.CounterPartyTypeId)
      .IsRequired();
    entityBuilder.HasIndex(e => e.Name).IsUnique();
  }
}