using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Transaction.Storage.Srv.API.WebApi.Controllers.AccountComponent;

public static class AccountSwaggerDocInit
{
    public const string Version = "v1.0";
    public const string ComponentName = nameof(AccountComponent);
    public static SwaggerGenOptions InitAccountApiInfo(this SwaggerGenOptions options)
    {
        options.SwaggerDoc(ComponentName, new OpenApiInfo
        {
            Version = Version,
            Title = $"{ComponentName} API",
            Description = $"An ASP.NET Core Web API service for getting information about {ComponentName}",
            Contact = new OpenApiContact
            {
                Name = "InsonusK",
                //Url = new Uri("https://github.com/Instrument-Data-Source/Instrument-quote-data-source-srv")
            }
        });
        return options;
    }
    public static SwaggerUIOptions InitAccountApiInfo(this SwaggerUIOptions options)
    {
        options.SwaggerEndpoint($"/swagger/{ComponentName}/swagger.json", ComponentName);
        return options;
    }
}