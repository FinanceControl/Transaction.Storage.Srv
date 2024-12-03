namespace Transaction.Storage.Srv.Shared.Model;

public interface IConstantDomainModel{
  public int Id{get;}
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