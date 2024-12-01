using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models;
using Xunit.Abstractions;
using static Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Models.Asset;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Test.Models;

public class DecimalSizeValidator_Test : LoggingTestsBase<Asset>
{

  public DecimalSizeValidator_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
  {

  }
  [Theory]
  [InlineData(0)]
  [InlineData(10)]
  [InlineData(15)]
  public void WHEN_decimal_len_from_0_to_15_THEN_ok(short usedDecimalSize)
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var validator = new DecimalSizeValidator();

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var assertedResult = validator.Validate(usedDecimalSize);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Assert.True(assertedResult.IsValid);

    #endregion
  }

  [Theory]
  [InlineData(-1)]
  [InlineData(16)]
  public void WHEN_decimal_len_NOT_from_0_to_15_THEN_ok(short usedDecimalSize)
  {
    #region Array
    Logger.LogDebug("Test ARRAY");

    var validator = new DecimalSizeValidator();

    #endregion


    #region Act
    Logger.LogDebug("Test ACT");

    var assertedResult = validator.Validate(usedDecimalSize);

    #endregion


    #region Assert
    Logger.LogDebug("Test ASSERT");

    Assert.False(assertedResult.IsValid);

    #endregion
  }
}
