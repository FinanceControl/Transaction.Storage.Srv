using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Transaction.Storage.Srv.Configurations.DataBase.Migrations.Factory;


public class ContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  private IConfigurationBuilder _configurationBuilder;
  private ILogger<AppDbContext> logger;
  public ContextFactory()
  {
    _configurationBuilder = new ConfigurationBuilder();
    _configurationBuilder.AddJsonFile("./appsettings.Development.json");

    var loggerFactory = LoggerFactory.Create(builder =>
    {
      // Add a console logger
      builder.AddConsole();
      // Other logging configurations can be added here
    });

    // Create a logger using the LoggerFactory
    logger = loggerFactory.CreateLogger<AppDbContext>();
  }

  public AppDbContext CreateDbContext(string[] args)
  {
    return CreateDbContext();
  }

  public AppDbContext CreateDbContext()
  {
    var _config = _configurationBuilder.Build();
    string connectionString = _config.GetConnectionString("DefaultConnection");

    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseNpgsql(connectionString);


    return new AppDbContext(optionsBuilder.Options, logger);
  }
}