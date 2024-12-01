using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using NSubstitute;
using Ardalis.Specification;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Entity;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Model;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Handlers;
using Transaction.Storage.Srv.App.Core.Aggregates.AccountAggregate.Events;

public class CounterPartyTypeFetchHandler_Test : LoggingTestsBase<CounterPartyTypeFetchHandler_Test>
{

    public CounterPartyTypeFetchHandler_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
    {

    }

    [Fact]
    public async Task WHEN_handle_fetch_THEN_return_mapped_resustAsync()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");

        var mockRepo = Substitute.For<IReadRepositoryBase<CounterPartyType>>();
        var testEntities = new List<CounterPartyType>
        {
            new CounterPartyType(ICounterPartyType.Enum.Storage),
            new CounterPartyType(ICounterPartyType.Enum.Individual)
        };

        mockRepo.ListAsync(Arg.Any<CancellationToken>()).Returns(testEntities);

        var handler = new CounterPartyTypeFetchHandler(mockRepo);
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
        Assert.Equal(2, resultList.Count);
        Assert.Equal(testEntities[0].Id, resultList[0].Id);
        Assert.Equal(testEntities[0].Name, resultList[0].Name);
        Assert.Equal(testEntities[1].Id, resultList[1].Id);
        Assert.Equal(testEntities[1].Name, resultList[1].Name);

        await mockRepo.Received(1).ListAsync(Arg.Any<CancellationToken>());

        #endregion
    }
}