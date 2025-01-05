using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using System.Text.Json;
using System.Text;
using System.Net;
using Transaction.Storage.Srv.App.Components.AssetComponent.Dtos;
using Transaction.Storage.Srv.API.WebApi.Controllers.AssetComponent;

namespace Transaction.Storage.Srv.API.WebApi.Test.Controllers.Asset;

public class AssetTypeController_Post_TestCases : LoggingTestsBase<AssetTypeController_Post_TestCases>, IDisposable, IClassFixture<ApplicationFactoryMock>
{
    private static string url = $"api/{AssetSwaggerDocInit.ComponentName}/AssetType";
    private readonly HttpClient _client;
    private readonly ApplicationFactoryMock application;

    public AssetTypeController_Post_TestCases(ITestOutputHelper output, ApplicationFactoryMock application, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
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
            Name = "Test Name",
            IsInflationProtected = false,
            IsUnderManagement = true
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
        var responseObject = JsonSerializer.Deserialize<AssetTypeDto>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(responseObject);
        Assert.Equal(usedDto.Name, responseObject.Name);
        Assert.Equal(usedDto.IsInflationProtected, responseObject.IsInflationProtected);
        Assert.Equal(usedDto.IsUnderManagement, responseObject.IsUnderManagement);
        

    }    

}