using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Entity;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Configs;
public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
  public void Configure(EntityTypeBuilder<Asset> builder)
  {
    builder
      .HasOne(e => e.AssetType)
      .WithMany(e => e.Assets)
      .HasForeignKey(e => e.AssetTypeId)
      .OnDelete(DeleteBehavior.Cascade)
      .IsRequired(); ;
    builder.HasIndex(e => e.Name).IsUnique();
  }
}