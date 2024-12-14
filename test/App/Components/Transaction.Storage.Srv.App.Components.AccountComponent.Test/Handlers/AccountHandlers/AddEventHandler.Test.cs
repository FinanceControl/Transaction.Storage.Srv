using Ardalis.Result;
using Ardalis.Specification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Transaction.Storage.Srv.App.Components.AccountComponent.Events.AccountEvents;
using Transaction.Storage.Srv.App.Components.AccountComponent.Handlers.AccountHandlers;
using Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;
using Transaction.Storage.Srv.Shared.Events.Interfaces;
using Transaction.Storage.Srv.Shared.Validators;
using Transaction.Storage.Srv.Test.Tools;
using Xunit.Abstractions;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Test.Handlers.AccountHandlers;
public class AddEventHandler_Test : BaseDbTest<AddEventHandler_Test>
{

    public AddEventHandler_Test(ITestOutputHelper output, LogLevel logLevel = LogLevel.Debug) : base(output, Module.Register, logLevel)
    {

    }

    [Fact]
    public async Task WHEN_handle_add_THEN_SavesToDatabaseAsync()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");
        var counterPartyMock = await new CounterPartyMocks(global_sp).AddAsync();

        var handler = new AccountAddEventHandler(
                                            global_sp.GetRequiredService<IRepositoryBase<Account>>(), 
                                            global_sp.GetRequiredService<IEntityFactory<AccountAddEvent, Account>>(),
                                            Output.BuildLoggerFor<AccountAddEventHandler>());
        var request = new AccountAddEvent
        {
            Name = "Test Account",
            Description = "Test account description",
            CounterPartyId = counterPartyMock.Id,
            IsUnderManagement = false,
            KeepassId = "123"     
        };

        var cancellationToken = CancellationToken.None;


        #endregion


        #region Act
        Logger.LogDebug("Test ACT");
        var assertedResult = await handler.Handle(request, cancellationToken);

        #endregion


        #region Assert
        Logger.LogDebug("Test ASSERT");

        Assert.True(assertedResult.IsSuccess);
        Assert.Equal(request.Name, assertedResult.Value.Name);
        Assert.True(assertedResult.Value.Id > 0);

        var savedEntities = await global_sp.GetRequiredService<IReadRepositoryBase<Account>>().ListAsync(cancellationToken);
        Assert.Single(savedEntities);

        var assertedEntity = savedEntities.First();
        Assert.Equal(assertedResult.Value.Id, assertedEntity.Id);
        Assert.Equal(request.Name, assertedEntity.Name);
        Assert.Equal(request.Description, assertedEntity.Description);
        Assert.Equal(request.CounterPartyId, assertedEntity.CounterPartyId);
        Assert.Equal(request.IsUnderManagement, assertedEntity.IsUnderManagement);
        Assert.Equal(request.KeepassId, assertedEntity.KeepassId);
        #endregion
    }

    [Fact]
    public async Task WHEN_InvalidCounterPartyId_THEN_ReturnError()
    {
        #region Array
        Logger.LogDebug("Test ARRAY");
        var counterPartyMock = await new CounterPartyMocks(global_sp).AddAsync();

        var handler = new AccountAddEventHandler(
                                            global_sp.GetRequiredService<IRepositoryBase<Account>>(), 
                                            global_sp.GetRequiredService<IEntityFactory<AccountAddEvent, Account>>(),
                                            Output.BuildLoggerFor<AccountAddEventHandler>());
        var request = new AccountAddEvent
        {
            Name = "Test Account",
            Description = "Test account description",
            CounterPartyId = counterPartyMock.Id+100,
            IsUnderManagement = false,
            KeepassId = "123"     
        };

        var cancellationToken = CancellationToken.None;


        #endregion


        #region Act
        Logger.LogDebug("Test ACT");
        var assertedResult = await handler.Handle(request, cancellationToken);

        #endregion


        #region Assert
        Logger.LogDebug("Test ASSERT");

        Assert.False(assertedResult.IsSuccess);
        Assert.Equal(ResultStatus.Invalid,assertedResult.Status);
        
        var assertedError = Assert.Single(assertedResult.ValidationErrors);
        Assert.Equal(IdValidator<Account>.DefaultCode, assertedError.ErrorCode);
        Assert.Equal(nameof(AccountAddEvent.CounterPartyId), assertedError.Identifier);
        Assert.Equal(ValidationSeverity.Error, assertedError.Severity);
        #endregion
    }
}
