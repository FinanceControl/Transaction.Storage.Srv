using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Models;
public class CounterPartyType_Test : LoggingTestsBase<CounterPartyType>
{

  public CounterPartyType_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
  {

  }

  [Fact]
  public void WHEN_request_list_THEN_get_all_list()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var asserted_list = CounterPartyType.ToList();

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");



    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Assert.NotEmpty(asserted_list);

    #endregion
  }
}