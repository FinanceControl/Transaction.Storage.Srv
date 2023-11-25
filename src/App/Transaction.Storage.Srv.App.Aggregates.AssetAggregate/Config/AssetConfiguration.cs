using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Config;
public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
  public void Configure(EntityTypeBuilder<Asset> builder)
  {
    builder
      .HasOne(e=>e.AssetType)
      .WithMany(e=>e.Assets)
      .HasForeignKey(e=>e.AssetTypeId)
      .OnDelete(DeleteBehavior.Cascade)
      .IsRequired();;
  }
}