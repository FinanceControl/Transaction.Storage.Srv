namespace Transcation.Storage.Srv.Shared.Database.Interfaces;

public interface IDomainEntityDto
{
  public int Id { get; }
  public DateTimeOffset CreatedDateTime { get; }

  public DateTimeOffset UpdatedDateTime { get; }
}