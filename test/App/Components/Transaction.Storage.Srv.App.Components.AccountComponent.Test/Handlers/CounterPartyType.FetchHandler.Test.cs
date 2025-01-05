using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using NSubstitute;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Model;
using Transaction.Storage.Srv.App.Components.AccountComponent.Handlers;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events;
using Transaction.Storage.Srv.Test.Tools;
using Microsoft.Extensions.DependencyInjection;
namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Handlers;
public class CounterPartyTypeFetchHandler_Test : BaseDbTest<CounterPartyTypeFetchHandler_Test>
{

    public CounterPartyTypeFetchHandler_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Module.Register, logLevel)
    {

    }

    [Fact]
    public async Task WHEN_handle_fetch_THEN_return_mapped_resustAsync()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");

        var repo = global_sp.GetRequiredService<IReadRepositoryBase<CounterPartyType>>();

        var handler = new CounterPartyTypeFetchHandler(repo);
        var request = new CounterPartyTypeFetchEvent();
        var cancellationToken = CancellationToken.None;

        #endregion


        #region Act
        Logger.LogDebug("Test ACT");

        var result = await handler.Handle(request, cancellationToken);

        #endregion


        #region Assert
        Logger.LogDebug("Test ASSERT");

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        var resultList = result.Value.ToList();
        Assert.Equal(3, resultList.Count);

        #endregion
    }
}