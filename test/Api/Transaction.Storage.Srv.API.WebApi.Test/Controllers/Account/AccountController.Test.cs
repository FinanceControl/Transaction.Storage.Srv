using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using System.Text.Json;
using System.Text;
using System.Net;
using Transaction.Storage.Srv.App.Components.AccountComponent.Dto;
using Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.API.WebApi.Controllers.AccountComponent;

namespace Transaction.Storage.Srv.API.WebApi.Test.Controllers.Account;

public class AccountController_Post_TestCases : LoggingTestsBase<AccountController_Post_TestCases>, IDisposable, IClassFixture<ApplicationFactoryMock>
{
    private static string url = $"api/{AccountSwaggerDocInit.ComponentName}/account";
    private readonly HttpClient _client;
    private CounterPartyDto _counterPartyDto;
    private readonly ApplicationFactoryMock application;

    public AccountController_Post_TestCases(ITestOutputHelper output, ApplicationFactoryMock application, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
    {
        this.application = application;
        _client = application.CreateClient();
    }

    [Fact]
    public async Task WHEN_ValidInput_THEN_ReturnsCreatedAsync()
    {
        // Arrange
        using (var scope_sp = application.Services.CreateScope())
        {
            _counterPartyDto = await new CounterPartyMocks(scope_sp.ServiceProvider).AddAsync();
        }

        var newAccountAddEvent = new
        {
            // Заполните свойства события
            Name = "Test Account",
            Description = "Some description",
            CounterPartyId = _counterPartyDto.Id,
            IsUnderManagement = true,
            KeepassId = "123"
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(newAccountAddEvent),
            Encoding.UTF8,
            "application/json");

        // Act
        var startDate = DateTime.UtcNow;
        var response = await _client.PostAsync(url, jsonContent);
        var endDate = DateTime.UtcNow;

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var assertedResponseString = await response.Content.ReadAsStringAsync();
        var assertedResponseObject = JsonSerializer.Deserialize<AccountDto>(assertedResponseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(assertedResponseObject);
        Assert.Equal("Test Account", assertedResponseObject.Name);
        Assert.InRange(assertedResponseObject.CreatedDateTime, startDate, endDate);
        Assert.InRange(assertedResponseObject.UpdatedDateTime, startDate, endDate);
    }

    [Fact]
    public async Task WHEN_InvalidCounterPartyId_THEN_ReturnsBadRequest()
    {
        // Arrange
        using (var scope_sp = application.Services.CreateScope())
        {
            _counterPartyDto = await new CounterPartyMocks(scope_sp.ServiceProvider).AddAsync();
        }

        var newAccountAddEvent = new
        {
            // Заполните свойства события
            Name = "Test Account",
            Description = "Some description",
            CounterPartyId = _counterPartyDto.Id+100,
            IsUnderManagement = true,
            KeepassId = "123"
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(newAccountAddEvent),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync(url, jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<ValidationProblemDetails>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(responseObject);
        Assert.Contains(nameof(newAccountAddEvent.CounterPartyId),responseObject.Errors);
    }

    
}