using Ardalis.Result;
using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AssetComponent.Entity;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.AssetComponent.Test.Models;

public class Asset_Test : LoggingTestsBase<Asset>
{

  public Asset_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
  {

  }
//#if DEBUG
//  [Theory]
//  [InlineData("22.2")]
//  [InlineData("22.22")]
//  [InlineData("22.222")]
//  [InlineData("0.2")]
//  [InlineData("0.22")]
//  [InlineData("0.222")]
//  public void WHEN_convert_correct_decimal_to_int_THEN_Ok(string usedDecimalStr)
//  {
//    #region Array
//    Logger.LogDebug("Test ARRAY");
//    var usedDecimal = decimal.Parse(usedDecimalStr);
//    var asset = new Asset(decimalSize: 3);
//
//    #endregion
//
//
//    #region Act
//    Logger.LogDebug("Test ACT");
//
//    var asserted_result = asset.checkDecimalLenght(usedDecimal);
//
//    #endregion
//
//
//    #region Assert
//    Logger.LogDebug("Test ASSERT");
//
//    Assert.True(asserted_result.IsSuccess);
//    Assert.Equal(ResultStatus.Ok, asserted_result.Status);
//
//    #endregion
//  }
//
//  [Fact]
//  public void WHEN_long_decimal_to_int_THEN_invalid()
//  {
//    #region Array
//    Logger.LogDebug("Test ARRAY");
//
//    var asset = new Asset(decimalSize: 3);
//
//    #endregion
//
//
//    #region Act
//    Logger.LogDebug("Test ACT");
//
//    var asserted_result = asset.checkDecimalLenght(22.2222M);
//
//    #endregion
//
//
//    #region Assert
//    Logger.LogDebug("Test ASSERT");
//
//    Assert.False(asserted_result.IsSuccess);
//    Assert.Equal(ResultStatus.Invalid, asserted_result.Status);
//    Assert.Single(asserted_result.ValidationErrors);
//
//    #endregion
//  }
//#endif
}