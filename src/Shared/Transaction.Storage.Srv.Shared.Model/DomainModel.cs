namespace Transaction.Storage.Srv.Shared.Model;

public interface IDomainModel
{
  /// <summary>
  /// Id in master system
  /// </summary>
  public int Id{get;set;}
  /// <summary>
  /// Guid of entity
  /// </summary>
  public Guid Guid { get; }
  public DateTimeOffset CreatedDateTime { get; }
  public DateTimeOffset UpdatedDateTime { get; }
}