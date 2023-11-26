using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Models;

namespace Transaction.Storage.Srv.App.Core.Aggregates.TransactionAggregate.Configs;
public class PositionConfig : IEntityTypeConfiguration<Position>
{
  public void Configure(EntityTypeBuilder<Position> entityBuilder)
  {
    //entityBuilder
    //  .HasOne(e => e.Header)
    //  .WithMany(e => e.Positions)
    //  .HasForeignKey(e => e.HeaderId)
    //  .IsRequired();
      
    entityBuilder
      .HasOne(e => e.Account)
      .WithMany()
      .HasForeignKey(e => e.AccountId);

    entityBuilder
      .HasOne(e => e.Asset)
      .WithMany()
      .HasForeignKey(e => e.AssetId)
      .IsRequired();
  }
}