namespace Transcation.Storage.Srv.Shared.Database.Interfaces;

public interface IDomainEntityDto : IEntityBaseDto
{

  public DateTimeOffset CreatedDateTime { get; }

  public DateTimeOffset UpdatedDateTime { get; }
}