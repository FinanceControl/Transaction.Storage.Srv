using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.CounterPartyEvents;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.Configurations.DataBase.Test;

public class AppDbContext_Test : BaseDbTest<AppDbContext>
{

  public AppDbContext_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Module.Register, logLevel)
  {

  }

  [Fact]
  public async void WHEN_add_new_record_THEN_update_creat_date_and_update_date()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    CounterParty used_CP;
    CancellationToken ct = default;
    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    DateTimeOffset fromDt = DateTimeOffset.UtcNow;
    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;
      var usedDbContext = sp.GetRequiredService<IRepositoryBase<CounterParty>>();
      var rr_cpt = sp.GetService<IReadRepositoryBase<CounterPartyType>>();
      var rr_cp = sp.GetService<IReadRepositoryBase<CounterParty>>();
      
      used_CP = await new CounterParty.Factory(rr_cpt,rr_cp).BuildAsync(new CounterPartyAddEvent()
      {
        Name = "N1",
        CounterPartyTypeId = 1
      });
      var used_counter_party = await usedDbContext.AddAsync(used_CP, ct);
    }
    DateTimeOffset untill = DateTimeOffset.UtcNow;
    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    using (var assert_scope = global_sp.CreateScope())
    {
      var sp = assert_scope.ServiceProvider;
      var usedDbContext = sp.GetRequiredService<IRepositoryBase<CounterParty>>();
      var asserted_CP = await usedDbContext.GetByIdAsync(used_CP.Id, default);

      Assert.NotNull(asserted_CP);
      Assert.Equal("N1", asserted_CP.Name);

      Assert.InRange(used_CP.CreatedDateTime, fromDt, untill);
      Assert.InRange(used_CP.UpdatedDateTime, fromDt, untill);

      Assert.Equal(used_CP.CreatedDateTime, used_CP.UpdatedDateTime);
    }
    #endregion
  }

  [Fact]
  public async void WHEN_update_record_THEN_update_only_update_date()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    CounterParty used_CP;
    DateTimeOffset fromCreatedDt = DateTimeOffset.UtcNow;
    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;
      var usedDbContext = sp.GetRequiredService<IRepositoryBase<CounterParty>>();
      var rr = sp.GetService<IReadRepositoryBase<CounterPartyType>>();
      var rr_cp = sp.GetService<IReadRepositoryBase<CounterParty>>();
      used_CP = await usedDbContext.AddAsync(await new CounterParty.Factory(rr,rr_cp).BuildAsync(new CounterPartyAddEvent()
      {
        Name = "N1",
        CounterPartyTypeId = 1
      }), default);
    }
    DateTimeOffset untillCreatedDt = DateTimeOffset.UtcNow;
    #endregion


    #region Act
    Logger.LogDebug("Test ACT");


    DateTimeOffset fromDt = DateTimeOffset.UtcNow;
    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;
      var usedDbContext = sp.GetRequiredService<IRepositoryBase<CounterParty>>();

      var changed_CP = await usedDbContext.GetByIdAsync(used_CP.Id, default);
      changed_CP.Name = "N2";

      await usedDbContext.SaveChangesAsync();
    }
    DateTimeOffset untill = DateTimeOffset.UtcNow;
    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");
    using (var assert_scope = global_sp.CreateScope())
    {
      var sp = assert_scope.ServiceProvider;
      var usedDbContext = sp.GetRequiredService<IRepositoryBase<CounterParty>>();
      var asserted_CP = await usedDbContext.GetByIdAsync(used_CP.Id, default);

      Assert.NotNull(asserted_CP);
      Assert.Equal("N2", asserted_CP.Name);
      Assert.InRange(asserted_CP.UpdatedDateTime, fromDt, untill);
      Assert.InRange(asserted_CP.CreatedDateTime, fromCreatedDt, untillCreatedDt);


      Logger.LogInformation($"Id = {asserted_CP.Id}\nVersion = {asserted_CP.Version}\nCreatedDateTime = {asserted_CP.CreatedDateTime}\nUpdated DateTime = {asserted_CP.UpdatedDateTime}");
    }
    #endregion
  }
}