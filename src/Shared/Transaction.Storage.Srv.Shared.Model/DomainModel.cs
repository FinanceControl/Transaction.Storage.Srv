namespace Transaction.Storage.Srv.Shared.Model;

/// <summary>
/// Simple model ID
/// </summary>
public interface IConstantDomainModelId{
  public int Id{get;}
}

/// <summary>
/// Domain ID model 
/// </summary>
public interface IDomainModelId: IConstantDomainModelId{
  /// <summary>
  /// Guid of entity
  /// </summary>
  public Guid Guid { get; }
}

/// <summary>
/// Base domain model with tech info
/// </summary>
public interface IDomainModel: IDomainModelId
{
  public DateTimeOffset CreatedDateTime { get; }
  public DateTimeOffset UpdatedDateTime { get; }
}