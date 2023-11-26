using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Transcation.Storage.Srv.Shared.Database;
using Transcation.Storage.Srv.Shared.Database.Models;
using Xunit.Abstractions;

namespace Transcation.Storage.Srv.Shared.Database.Test.Models;

public class EnumEntity_Test : LoggingTestsBase<EnumEntity<EnumEntity_Test.TestEnum>>
{
  public enum TestEnum
  {
    E1 = 1,
    E2 = 2
  }
  public EnumEntity_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
  {

  }

  [Fact]
  public void WHEN_request_name_THEN_get_value()
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var ee = new EnumEntity<EnumEntity_Test.TestEnum>(1);

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");



    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Assert.Equal("E1", ee.Name);

    #endregion
  }
}