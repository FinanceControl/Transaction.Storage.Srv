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
using Microsoft.AspNetCore.Mvc;

namespace Transaction.Storage.Srv.API.WebApi.Test.Controllers.Account;

public class CounterPartyController_Post_TestCases : LoggingTestsBase<CounterPartyController_Post_TestCases>, IDisposable, IClassFixture<ApplicationFactoryMock>
{
    private static string url = $"api/{SwaggerGenOptionsInit.AccountAggregate}/counterparty";
    private readonly HttpClient _client;
    private CounterPartyDto _counterPartyDto;
    private readonly ApplicationFactoryMock application;

    public CounterPartyController_Post_TestCases(ITestOutputHelper output, ApplicationFactoryMock application, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
    {
        this.application = application;
        _client = application.CreateClient();
    }

    [Fact]
    public async Task WHEN_ValidInput_THEN_ReturnsCreatedAsync()
    {
        var usedDto = new
        {
            // Заполните свойства события
            Name = "Test CP",
            CounterPartyTypeId = 1,
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(usedDto),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync(url, jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<CounterPartyDto>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(responseObject);
        Assert.Equal("Test CP", responseObject.Name);

    }

    [Fact]
    public async Task WHEN_InvalidCounterPartyId_THEN_ReturnsBadRequest()
    {
        // Arrange
        var usedDto = new
        {
            // Заполните свойства события
            Name = "Test CP",
            CounterPartyTypeId = 100,
        };  

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(usedDto),
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
        Assert.Contains(nameof(usedDto.CounterPartyTypeId),responseObject.Errors);
    }

}