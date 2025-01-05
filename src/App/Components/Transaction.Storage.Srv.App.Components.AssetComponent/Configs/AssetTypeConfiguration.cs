using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Configs;
public class AssetTypeConfiguration : IEntityTypeConfiguration<AssetType>
{
  public void Configure(EntityTypeBuilder<AssetType> builder)
  {
    builder.HasIndex(e => e.Name).IsUnique();
  }
}