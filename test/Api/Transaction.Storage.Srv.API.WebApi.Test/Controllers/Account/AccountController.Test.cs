using System;
using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text.Json;
using System.Text;
using System.Net;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;
using Transaction.Storage.Srv.App.Components.AccountComponent.Entity;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.Configurations.DataBase;

namespace Transaction.Storage.Srv.API.WebApi.Test.Controllers.Account;

public class AccountController_Test : LoggingTestsBase<AccountController_Test>, IDisposable, IClassFixture<ApplicationFactoryMock>
{

    private readonly HttpClient _client;
    private CounterPartyDto _counterPartyDto;
    private readonly ApplicationFactoryMock application;

    public AccountController_Test(ITestOutputHelper output, ApplicationFactoryMock application, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
    {
        this.application = application;
        _client = application.CreateClient();
    }
    [Fact]
    public async Task PostAccount_WHEN_ValidInput_THEN_ReturnsCreatedAsync()
    {
        using (var scope_sp = application.Services.CreateScope())
        {
            //var mockTask = new CounterPartyMocks(scope_sp.ServiceProvider).AddAsync();
            //mockTask.Wait();
            _counterPartyDto = await new CounterPartyMocks(scope_sp.ServiceProvider).AddAsync();
        }
        // Arrange
        var newAccountAddEvent = new
        {
            // Заполните свойства события
            Name = "Test Account",
            CounterPartyId = _counterPartyDto.Id
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(newAccountAddEvent),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/api/account", jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<AccountDto>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(responseObject);
        Assert.Equal("Test Account", responseObject.Name);

    }

    public virtual void Dispose()
    {
        application.Dispose();
    }
}