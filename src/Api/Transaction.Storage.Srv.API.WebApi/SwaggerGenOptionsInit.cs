using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Transaction.Storage.Srv.API.WebApi;
public static class SwaggerGenOptionsInit
{
  public const string Version = "v1.0";
  public const string AssetAggregate = nameof(App.Core.Aggregates.AssetAggregate);
  public const string AccountAggregate = nameof(App.Core.Aggregates.AccountAggregate);
  public const string TransactionAggregate = nameof(App.Core.Aggregates.TransactionAggregate);

  public static void Init(this SwaggerGenOptions options)
  {
    options.SwaggerDoc(AccountAggregate, new OpenApiInfo
    {
      Version = Version,
      Title = $"{AccountAggregate} API",
      Description = $"An ASP.NET Core Web API service for getting information about {AccountAggregate}",
      Contact = new OpenApiContact
      {
        Name = "InsonusK",
        //Url = new Uri("https://github.com/Instrument-Data-Source/Instrument-quote-data-source-srv")
      }
    });
    options.SwaggerDoc(AssetAggregate, new OpenApiInfo
    {
      Version = Version,
      Title = $"{AssetAggregate} API",
      Description = $"An ASP.NET Core Web API service for getting information about {AssetAggregate}",
      Contact = new OpenApiContact
      {
        Name = "InsonusK",
        //Url = new Uri("https://github.com/Instrument-Data-Source/Instrument-quote-data-source-srv")
      }
    });
    options.SwaggerDoc(TransactionAggregate, new OpenApiInfo
    {
      Version = Version,
      Title = $"{TransactionAggregate} API",
      Description = $"An ASP.NET Core Web API service for getting information about {TransactionAggregate}",
      Contact = new OpenApiContact
      {
        Name = "InsonusK",
        //Url = new Uri("https://github.com/Instrument-Data-Source/Instrument-quote-data-source-srv")
      }
    });
  }

  public static void Init(this SwaggerUIOptions options)
  {
    options.SwaggerEndpoint($"/swagger/{AssetAggregate}/swagger.json", AssetAggregate);
    options.SwaggerEndpoint($"/swagger/{AccountAggregate}/swagger.json", AccountAggregate);
    options.SwaggerEndpoint($"/swagger/{TransactionAggregate}/swagger.json", TransactionAggregate);
  }
}