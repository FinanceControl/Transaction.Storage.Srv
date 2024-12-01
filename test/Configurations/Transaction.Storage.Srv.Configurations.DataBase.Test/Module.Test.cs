using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;
using Transaction.Storage.Srv.Configurations.DataBase.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.Configurations.DataBase.Test;

public class Module_Test : BaseDbTest<AppDbContext>
{

  public Module_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Module.Register, logLevel)
  {

  }

  [Fact]
  public async Task WHEN_init_THEN_CounterPartyType_contain_valuesAsync()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");



    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    List<CounterPartyType> assertedResult;
    List<CounterPartyType> assertedResult2;
    using (var act_scope = global_sp.CreateScope())
    {
      var sp = act_scope.ServiceProvider;
      var usedDbContext = sp.GetRequiredService<AppDbContext>();
      assertedResult = usedDbContext.CounterPartyTypes.ToList();

      var rr = sp.GetService<IReadRepositoryBase<CounterPartyType>>();
      assertedResult2 = await rr.ListAsync();
    }

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Assert.Equal(Enum.GetNames<ICounterPartyType.Enum>().Count(), assertedResult.Count);
    foreach (var cpt in assertedResult)
    {
      var expected_name = Enum.GetName<ICounterPartyType.Enum>(cpt.EnumId);
      Assert.Equal(expected_name, cpt.Name);
    }

    Assert.Equal(Enum.GetNames<ICounterPartyType.Enum>().Count(), assertedResult2.Count);
    foreach (var cpt in assertedResult2)
    {
      var expected_name = Enum.GetName<ICounterPartyType.Enum>(cpt.EnumId);
      Assert.Equal(expected_name, cpt.Name);
    }
    #endregion
  }
}