using Divergic.Logging.Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit.Abstractions;
using Transaction.Storage.Srv.Configurations.DataBase;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace Transaction.Storage.Srv.Test.Tools;

public abstract class BaseDbTest<T> : LoggingTestsBase<T>, IDisposable
{
  protected IServiceProvider global_sp;
  private static IConfiguration GetConfig(string dbSuffix)
  {
    var _configurationBuilder = new ConfigurationBuilder();
    int next_number;
    setupConfigBuider(_configurationBuilder);

    return _configurationBuilder.Build();
  }

  private static void setupConfigBuider(IConfigurationBuilder _configurationBuilder)
  {
    _configurationBuilder.AddJsonFile("./appsettings.test.json");
    _configurationBuilder.AddEnvironmentVariables();

    // Извлечь строку подключения
    var connectionString = _configurationBuilder.Build().GetConnectionString("DefaultConnection");

    // Модифицировать строку подключения, добавив имя теста и GUID
    var testGuid = Guid.NewGuid();
    var conStrBuilder = new NpgsqlConnectionStringBuilder(connectionString);
    conStrBuilder.Database = $"{conStrBuilder.Database}_{testGuid}";
    var newConnectionString = conStrBuilder.ConnectionString;

    var dict = new Dictionary<string, string>
      {
          {"ConnectionStrings:DefaultConnection", newConnectionString}
      };
    _configurationBuilder.AddInMemoryCollection(dict);

  }
  public BaseDbTest(ITestOutputHelper output, Func<IServiceCollection,IConfiguration, IServiceCollection> serviceRegistration, LogLevel logLevel) :
    this(output, (sc,config) => { serviceRegistration(sc,config); }, logLevel)
  {

  }
  public BaseDbTest(ITestOutputHelper output, IEnumerable<Func<IServiceCollection,IConfiguration, IServiceCollection>> serviceRegistrationArr, LogLevel logLevel) :
    this(output, (sc,config) =>
    {
      foreach (var serviceRegistration in serviceRegistrationArr)
        serviceRegistration(sc,config);
    }, logLevel)
  {

  }
  public BaseDbTest(ITestOutputHelper output, Action<IServiceCollection,IConfiguration> serviceRegistration, LogLevel logLevel) : base(output, logLevel)
  {

    var host = new HostBuilder()
          //.ConfigureHostConfiguration(config => setupConfigBuider(typeof(T).Name, config))
          .ConfigureServices((hostContext, services) =>
          {
            // Add required services to the ServiceCollection
            var hostEnvironment = Substitute.For<IHostEnvironment>();
            hostEnvironment.EnvironmentName.Returns("Test");

            services.AddSingleton<IHostEnvironment>(hostEnvironment);
            services.AddSingleton<IConfiguration>(GetConfig(typeof(T).Name));
            services.AddLogging(builder =>
                  {
                    builder.AddXunit(output); // Add the xUnit logger
                  });
            services.AddSingleton<ILogger>(sp => output.BuildLogger());
            Module.Register(services, services.BuildServiceProvider().GetRequiredService<IConfiguration>());
            serviceRegistration(services,services.BuildServiceProvider().GetRequiredService<IConfiguration>());
          })
          .Build();

    global_sp = host.Services;
    var db_context = global_sp.GetRequiredService<AppDbContext>();
    db_context.Database.Migrate();
  }


  public virtual void Dispose()
  {
    DeleteDb();
  }

  private void DeleteDb()
  {
    using var scope = global_sp.CreateScope();
    var _sp = scope.ServiceProvider;
    _sp.DeleteDb();
  }
}
