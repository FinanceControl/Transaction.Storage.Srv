using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using System.Text.Json;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Storage.Srv.App.Components.AssetComponent.Test.Mocks;
using Transaction.Storage.Srv.API.WebApi.Controllers.AssetComponent;

namespace Transaction.Storage.Srv.API.WebApi.Test.Controllers.Asset;

public class AssetController_Post_TestCases : LoggingTestsBase<AssetController_Post_TestCases>, IDisposable, IClassFixture<ApplicationFactoryMock>
{
    private static string url = $"api/{AssetSwaggerDocInit.ComponentName}/Asset";
    private readonly HttpClient _client;
    private readonly ApplicationFactoryMock application;
    private AssetTypeDto assetTypeDto;
    public AssetController_Post_TestCases(ITestOutputHelper output, ApplicationFactoryMock application, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
    {
        this.application = application;
        _client = application.CreateClient();
    }

    [Fact]
    public async Task WHEN_ValidInput_THEN_ReturnsCreatedAsync()
    {
        using (var scope_sp = application.Services.CreateScope())
        {
            assetTypeDto = await new AssetTypeMocks(scope_sp.ServiceProvider).AddAsync();
        }
        var usedDto = new
        {
            // Заполните свойства события
            Name = "Test Name",
            AssetTypeId = assetTypeDto.Id,
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
        var responseObject = JsonSerializer.Deserialize<AssetDto>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(responseObject);
        Assert.Equal(usedDto.Name, responseObject.Name);

    }

    [Fact]
    public async Task WHEN_InvalidAssetId_THEN_ReturnsBadRequest()
    {
        // Arrange
        var usedDto = new
        {
            // Заполните свойства события
            Name = "Test CP",
            AssetTypeId = 100,
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
        Assert.Contains(nameof(usedDto.AssetTypeId),responseObject.Errors);
    }

}