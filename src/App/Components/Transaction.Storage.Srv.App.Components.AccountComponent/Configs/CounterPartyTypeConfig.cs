using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Configs;
public class CounterPartyTypeConfig : IEntityTypeConfiguration<CounterPartyType>
{
  public void Configure(EntityTypeBuilder<CounterPartyType> entityBuilder)
  {
    foreach (var tf in CounterPartyType.ToList())
    {
      entityBuilder.HasData(new CounterPartyType(tf.EnumId));
    }
  }
}