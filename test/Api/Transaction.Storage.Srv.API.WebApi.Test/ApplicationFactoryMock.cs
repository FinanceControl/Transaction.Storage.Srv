using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Transaction.Storage.Srv.Configurations.DataBase;

namespace Transaction.Storage.Srv.API.WebApi.Test;

public class ApplicationFactoryMock : WebApplicationFactory<Program>
{

  public HttpClient CreateClientForTest() => CreateClient();
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureAppConfiguration((context, config) =>
    {
      config.SetBasePath(Directory.GetCurrentDirectory());
      // Загрузить настройки из appsettings.Test.json
      config.AddJsonFile("appsettings.Test.json", optional: false);

      // Построить конфигурацию
      var builtConfig = config.Build();

      // Извлечь строку подключения
      var connectionString = builtConfig.GetConnectionString("DefaultConnection");

      // Модифицировать строку подключения, добавив имя теста и GUID
      var testGuid = Guid.NewGuid();
      var conStrBuilder = new NpgsqlConnectionStringBuilder(connectionString);
      conStrBuilder.Database = $"{conStrBuilder.Database}_{testGuid}";
      var newConnectionString = conStrBuilder.ConnectionString;

      // Передать модифицированную строку подключения
      config.AddInMemoryCollection(
      [
          new KeyValuePair<string, string>("ConnectionStrings:DefaultConnection", newConnectionString)
      ]);
    });
    builder.UseEnvironment("Test");
    builder.ConfigureServices(services =>
    {
      var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
      // Настроить контекст базы данных с новой строкой подключения
      services.AddDbContext<AppDbContext>(options =>
          options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    });
  }

  public new virtual void Dispose()
  {
    using var scope = this.Services.CreateScope();
    var _sp = scope.ServiceProvider;
    _sp.DeleteDb();
    base.Dispose();
  }
}