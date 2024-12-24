using Transaction.Storage.Srv.Shared.Model;

namespace Transaction.Storage.Srv.App.Components.AccountComponent.Model;
public interface IAccount : IDomainModel, IAccountBody
{
  public DateOnly CloseDate { get; }
  public DateOnly LastSyncDate { get; }
}


public interface IAccountBody
{
  public string Name { get; }

  public string KeepassId { get; }

  public string Description { get; }

  public int CounterPartyId { get; }
  public bool IsUnderManagement { get; }
  public string ExternalId {get;}
}

/// <summary>
/// Calculated fields
/// </summary>
public interface IAccountInfo
{
  public bool IsClosed { get; }
  public bool IsEmpty { get; }
  public DateTime LastOperation { get; }
  public int DaysFromLastSync { get; }
}
