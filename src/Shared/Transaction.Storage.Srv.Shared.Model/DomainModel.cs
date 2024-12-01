namespace Transaction.Storage.Srv.Shared.Model;

public interface IConstantDomainModel{
  /// <summary>
  /// Id in master system
  /// </summary>
  public int Id{get;set;}
}

public interface IDomainModel: IConstantDomainModel
{
  /// <summary>
  /// Guid of entity
  /// </summary>
  public Guid Guid { get; }
  public DateTimeOffset CreatedDateTime { get; }
  public DateTimeOffset UpdatedDateTime { get; }
}