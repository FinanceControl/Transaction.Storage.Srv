using Transaction.Storage.Srv.Shared.Database.Interfaces;

namespace Transaction.Storage.Srv.App.Core.Aggregates.AssetAggregate.Interfaces;

public interface IAssetTypeDto : IDomainEntityDto
{
  public string Name { get; }
  public bool IsInflationProtected { get;  }

  public bool IsUnderManagement { get;  }
}