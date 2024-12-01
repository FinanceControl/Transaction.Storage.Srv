namespace Transaction.Storage.Srv.Shared.Model;

public interface IDomainModel
{
  /// <summary>
  /// Unique id of entity
  /// </summary>
  public int Id { get; }
  public DateTimeOffset CreatedDateTime { get; }
  public DateTimeOffset UpdatedDateTime { get; }
}