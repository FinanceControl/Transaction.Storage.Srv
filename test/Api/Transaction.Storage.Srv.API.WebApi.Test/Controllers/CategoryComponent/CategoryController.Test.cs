using Divergic.Logging.Xunit;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using System.Text.Json;
using System.Text;
using System.Net;
using Transaction.Storage.Srv.API.WebApi.Controllers.CategoryComponent;
using Transaction.Storage.Srv.App.Components.CategoryComponent.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Transaction.Storage.Srv.App.Components.AccountComponent.Test.Mocks;

namespace Transaction.Storage.Srv.API.WebApi.Test.Controllers.Asset;

public class CategoryController_Post_TestCases : LoggingTestsBase<CategoryController_Post_TestCases>, IDisposable, IClassFixture<ApplicationFactoryMock>
{
    private static string url = $"api/{CategorySwaggerDocInit.ComponentName}/Category";
    private readonly HttpClient _client;
    private readonly ApplicationFactoryMock application;

    public CategoryController_Post_TestCases(ITestOutputHelper output, ApplicationFactoryMock application, LogLevel logLevel = LogLevel.Debug) : base(output, logLevel)
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
            Name = "Test Name"
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(usedDto),
            Encoding.UTF8,
            "application/json");

        // Act
        var startDate = DateTime.UtcNow;
        var response = await _client.PostAsync(url, jsonContent);
        var endDate = DateTime.UtcNow;

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var assertedResponseObject = JsonSerializer.Deserialize<CategoryDto>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(assertedResponseObject);
        Assert.Equal(usedDto.Name, assertedResponseObject.Name);
        Assert.InRange(assertedResponseObject.CreatedDateTime, startDate, endDate);
        Assert.InRange(assertedResponseObject.UpdatedDateTime, startDate, endDate);        
    }  

    [Fact]
    public async Task WHEN_ValidDuplicateInput_THEN_ReturnsCreatedAsync()
    {
        CategoryDto existCategoryDto;
        using (var scope_sp = application.Services.CreateScope())
        {
            existCategoryDto = await new CategoryMocks(scope_sp.ServiceProvider).AddAsync();
        }

        var usedDto = new
        {
            // Заполните свойства события
            Name = existCategoryDto.Name
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(usedDto),
            Encoding.UTF8,
            "application/json");

        // Act
        var startDate = DateTime.UtcNow;
        var response = await _client.PostAsync(url, jsonContent);
        var endDate = DateTime.UtcNow;

        // Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var assertedResponseObject = JsonSerializer.Deserialize<ValidationProblemDetails>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(assertedResponseObject);        
    }   

}