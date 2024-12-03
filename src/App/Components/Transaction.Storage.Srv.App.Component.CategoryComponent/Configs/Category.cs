using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Component.CategoryComponent.Entities;

namespace Transaction.Storage.Srv.App.Component.CategoryComponent.Configs;

public class AssetConfiguration : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.HasIndex(e => e.Name).IsUnique();
  }
}