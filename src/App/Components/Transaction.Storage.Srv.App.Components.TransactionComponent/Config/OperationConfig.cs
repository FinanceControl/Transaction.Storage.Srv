using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Transaction.Storage.Srv.App.Components.BudgetComponent.Entity;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Entity;
using Transaction.Storage.Srv.App.Components.TransactionComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.TransactionComponent.Configs;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasOne<Account>().WithMany().HasForeignKey(e=>e.AccountId).IsRequired();
        builder.HasOne<Budget>().WithMany().HasForeignKey(e=>e.BudgetId).IsRequired();
        builder.HasOne<Category>().WithMany().HasForeignKey(e=>e.CategoryId).IsRequired();
        builder.HasOne<Asset>().WithMany().HasForeignKey(e=>e.AssetId).IsRequired();
    }
}