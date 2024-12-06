using Divergic.Logging.Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit.Abstractions;
using Transaction.Storage.Srv.Configurations.DataBase;

namespace Transaction.Storage.Srv.Test.Tools;

public abstract class BaseDbTest<T> : LoggingTestsBase<T>, IDisposable
{
  protected IServiceProvider global_sp;
  private static IConfiguration GetConfig(string dbSuffix)
  {
    var _configurationBuilder = new ConfigurationBuilder();
    int next_number;
    setupConfigBuider(dbSuffix + Guid.NewGuid().ToString(), _configurationBuilder);

    return _configurationBuilder.Build();
  }

  private static void setupConfigBuider(string dbSuffix, IConfigurationBuilder _configurationBuilder)
  {
    _configurationBuilder.AddJsonFile("./appsettings.test.json");
    _configurationBuilder.AddEnvironmentVariables();
    var dict = new Dictionary<string, string>
      {
          {"ConnectionStrings:DbSuffix", dbSuffix},
          {"ASPNETCORE_ENVIRONMENT", "Development"}
      };
    _configurationBuilder.AddInMemoryCollection(dict);

    //Console.WriteLine(_configurationBuilder.Build().GetConnectionString("DefaultConnection"));
    //Console.WriteLine("Db Suffix: " + dbSuffix);
  }
  public BaseDbTest(ITestOutputHelper output, Func<IServiceCollection, IServiceCollection> serviceRegistration, LogLevel logLevel) :
    this(output, (sc) => { serviceRegistration(sc); }, logLevel)
  {

  }
  public BaseDbTest(ITestOutputHelper output, IEnumerable<Func<IServiceCollection, IServiceCollection>> serviceRegistrationArr, LogLevel logLevel) :
    this(output, (sc) => { 
      foreach (var serviceRegistration in serviceRegistrationArr)
        serviceRegistration(sc);           
      }, logLevel)
  {

  }
  public BaseDbTest(ITestOutputHelper output, Action<IServiceCollection> serviceRegistration, LogLevel logLevel) : base(output, logLevel)
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
            Module.Register(services);
            serviceRegistration(services);
          })
          .Build();

    global_sp = host.Services;
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
