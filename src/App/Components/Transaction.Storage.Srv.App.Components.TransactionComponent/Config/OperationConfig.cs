using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Configs;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasOne(r=>r.Account).WithMany().HasForeignKey(e=>e.AccountId).IsRequired();
        builder.HasOne(e=>e.Budget).WithMany().HasForeignKey(e=>e.BudgetId).IsRequired();
        builder.HasOne(e=>e.Category).WithMany().HasForeignKey(e=>e.CategoryId).IsRequired();
        builder.HasOne(e=>e.Asset).WithMany().HasForeignKey(e=>e.AssetId).IsRequired();
    }
}