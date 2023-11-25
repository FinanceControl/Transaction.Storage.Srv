using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Transaction.Storage.Srv.Configuration.DataBase.Migrations.Factory;


public class ContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  private IConfigurationBuilder _configurationBuilder;

  public ContextFactory()
  {
    _configurationBuilder = new ConfigurationBuilder();
    _configurationBuilder.AddJsonFile("./appsettings.Development.json");
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

    return new AppDbContext(optionsBuilder.Options);
  }
}